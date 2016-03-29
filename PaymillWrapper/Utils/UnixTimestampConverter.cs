using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Reflection;
using System.Globalization;

namespace PaymillWrapper.Utils
{
    class UnixTimestampConverter : DateTimeConverterBase
    {
        private static bool IsNullableType(Type type)
        {
            return type.GetType().GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public override object ReadJson(JsonReader reader, Type objectType,
            object existingValue, JsonSerializer serializer)
        {
            var t = IsNullableType(objectType)
                ? Nullable.GetUnderlyingType(objectType)
                : objectType;
            if (reader.TokenType == JsonToken.Null)
            {
                if (!IsNullableType(objectType))
                {
                    throw new Exception(String.Format(CultureInfo.InvariantCulture, "Cannot convert null value to {0}.",
                        new object[] { objectType }));
                }
                return null;
            }
            if (reader.TokenType != JsonToken.Integer)
            {
                throw new Exception(String.Format(CultureInfo.InvariantCulture, "Unexpected token parsing date. Expected Integer, got {0}.",
                    new object[] { reader.TokenType }));
            }
            var ticks = (long)reader.Value;
            var d = ticks.ParseAsUnixTimestamp();
            if (t == typeof(DateTimeOffset))
            {
                return new DateTimeOffset(d);
            }
            return d;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            long ticks;
            if (value is DateTime)
            {
                ticks = ((DateTime)value).ToUniversalTime().ToUnixTimestamp();
            }
            else
            {
                if (!(value is DateTimeOffset))
                {
                    throw new Exception("Expected date object value.");
                }
                var dateTimeOffset = (DateTimeOffset)value;
                ticks = dateTimeOffset.ToUniversalTime().UtcDateTime.ToUnixTimestamp();
            }
            writer.WriteValue(ticks);
        }
    }
}
