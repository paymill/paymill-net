using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PaymillWrapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PaymillWrapper.Utils
{
    /// <summary>
    /// Convert json object to object of type T. If converts failed it tries to create object with explicit contructor and set property Id
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StringToBaseModelConverter<T> : JsonConverter
    {
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
            throw new NotImplementedException();
        }
    }
    public class StringToWebhookEventTypeConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(PaymillWrapper.Models.Webhook.WebhookEventType))
                return true;

            return false;
        }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            String value = reader.Value.ToString();
            return PaymillWrapper.Models.Webhook.WebhookEventType.GetEventByName(value);

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
