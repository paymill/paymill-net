using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PaymillWrapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


namespace PaymillWrapper.Net
{
    public class JsonParser<T> : JsonConverter
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
}
