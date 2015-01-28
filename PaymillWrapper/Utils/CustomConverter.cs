using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using PaymillWrapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PaymillWrapper.Utils
{
    class JsonConverterExclusionResolver<T> : DefaultContractResolver
    {
        protected override JsonConverter ResolveContractConverter(Type objectType)
        {
            JsonConverter conv = base.ResolveContractConverter(objectType);
            if (conv != null && conv.GetType() == typeof(T))
            {
                // if something asks for the converter we're excluding,
                // we never heard of it
                return null;
            }
            return conv;
        }
    }
    /// <summary>
    /// Convert json object to object of type T. If converts failed it tries to create object with explicit contructor and set property Id
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StringToBaseModelConverter<T> : JsonConverter
    {
        private IContractResolver exclusionResolver = new JsonConverterExclusionResolver<StringToBaseModelConverter<T>>();
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(BaseModel))
                return true;

            return false;
        }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                JObject jObject = JObject.Load(reader);
                T target = (T)Activator.CreateInstance(typeof(T));
                serializer.Populate(jObject.CreateReader(), target);
                return target;
            }
            catch
            {
                JArray ja = new JArray();

                T target = (T)Activator.CreateInstance(typeof(T));

                if (reader.Value != null)
                {
                    PropertyInfo prop = target.GetType().GetProperty("Id", BindingFlags.Public | BindingFlags.Instance);
                    if (null != prop && prop.CanWrite)
                        prop.SetValue(target, reader.Value.ToString(), null);
                }

                return target;
            }

        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {

            JsonSerializer innerSerializer = new JsonSerializer();
            innerSerializer.ContractResolver = exclusionResolver;
            // (copy other settings from the outer serializer if needed)

            var o = JObject.FromObject(value, innerSerializer);

            // ...do your custom stuff here...

            o.WriteTo(writer);
        }
    }
    public class StringToNIntConverter : JsonConverter
    {

        public override bool CanConvert(Type objectType)
        {
            return ( objectType == typeof(int));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return default(int);
            if (reader.TokenType == JsonToken.Integer)
                return Convert.ToInt32(reader.Value);

            if (reader.TokenType == JsonToken.String)
            {
                if (string.IsNullOrEmpty((string)reader.Value))
                    return default(int);
                int num;
                if (int.TryParse((string)reader.Value, out num))
                    return num;

                throw new JsonReaderException(string.Format("Expected integer, got {0}", reader.Value));
            }
            throw new JsonReaderException(string.Format("Unexcepted token {0}", reader.TokenType));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value);
        }
    }
    public class StringToBaseEnumTypeConverter<T> : Newtonsoft.Json.JsonConverter  where T : EnumBaseType
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(T))
                return true;

            return false;
        }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value != null)
            {
                String value = reader.Value.ToString();
                return EnumBaseType.GetItemByValue(value, typeof(T));
            }
            return null;

        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteRawValue(value.ToString());
        }
    }

    public class StringToPeriodWithChargeDaydConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(String))
                return true;

            return false;
        }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            String value = reader.Value.ToString();
            return new Interval.PeriodWithChargeDay(value);

        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteRawValue(value.ToString());
        }
    }
    public class StringToPeriodConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(String))
                return true;

            return false;
        }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value != null)
            {
                String value = reader.Value.ToString();
                return new Interval.Period(value);
            }
            else
                return null;

        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteRawValue(value.ToString());
        }
    }
    public class StringToIntervalConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(String))
                return true;

            return false;
        }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            String value = reader.Value.ToString();
            return new Interval(value);

        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteRawValue(value.ToString());
        }
    }
}
