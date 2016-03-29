using PaymillWrapper.Exceptions;
using PaymillWrapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;

namespace PaymillWrapper.Utils
{
    public class UrlEncoder
    {
        private Encoding charset;

        public UrlEncoder()
        {
            charset = Encoding.UTF8;
        }

        /// <summary>
        /// Encodes the specified data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        /// <exception cref="PaymillWrapper.Exceptions.PaymillException"></exception>
        public string Encode<T>(Object data)
        {
            var props = typeof(T).GetProperties();

            if (!data.GetType().ToString().StartsWith("PaymillWrapper.Models"))
                throw new PaymillException(
                    String.Format("Unknown object type '{0}'. Only objects in package " +
                    "'PaymillWrapper.Paymill.Model' are supported.", data.GetType().ToString())
                    );

            StringBuilder sb = new StringBuilder();
            foreach (var prop in props)
            {
                object value = prop.GetValue(data, null);
                if (value != null)
                    this.addKeyValuePair(sb, prop.Name.ToLower(), value);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Encodes the ParamsMap.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public string EncodeParamsMap<K, V>(ParameterMap<K, V> data)
                where K : class
                where V : class
        {

            StringBuilder sb = new StringBuilder();
            foreach (var key in data.KeyCollection())
            {
                List<V> values = data.Get(key);
                object value = String.Join(",", values);
                if (value != null)
                {
                    this.addKeyValuePair(sb, key.ToString().ToLower(), value);
                }
            }

            return sb.ToString();
        }
        /// <summary>
        /// Encodes the object.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public string EncodeObject(Object data)
        {
            if (data.GetType().Name.Contains("AnonymousType") == false)
            {
                throw new ArgumentException("Invalid object to encode");
            }
            var props = data.GetType().GetProperties();
            StringBuilder sb = new StringBuilder();
            foreach (var prop in props)
            {
                object value = prop.GetValue(data, null);
                if (value != null)
                {
                    this.addKeyValuePair(sb, prop.Name.ToLower(), value);
                }
            }

            return sb.ToString();
        }
        /// <summary>
        /// Encodes the update.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public string EncodeUpdate(Object data)
        {
            var props = data.GetType().GetProperties();
            StringBuilder sb = new StringBuilder();
            var updatebles = props.Where(x => x.GetCustomAttributes(typeof(Updateable), false).Count() > 0);
            foreach (var prop in updatebles)
            {
                object value = prop.GetValue(data, null);
                var updateProps = (Updateable)prop.GetCustomAttributes(typeof(Updateable), false).First();
                if (updateProps.OnlyProperty != null && value != null)
                {
                    var valueProp = value.GetType().GetProperty(updateProps.OnlyProperty);
                    value = valueProp.GetValue(value, null);
                }
                if (value != null)
                {
                    if (value is Boolean)
                    {
                        value = value.ToString().ToLower();
                    }
                    this.addKeyValuePair(sb, updateProps.Name.ToLower(), value.ToString());
                }
            }

            return sb.ToString();
        }
        /// <summary>
        /// Converts the events arr.
        /// </summary>
        /// <param name="eventTypes">The event types.</param>
        /// <returns></returns>
        public static String ConvertEventsArr(params EnumBaseType[] eventTypes)
        {
            List<String> typesList = new List<String>();
            foreach (EnumBaseType evt in eventTypes)
            {
                typesList.Add(evt.ToString());
            }

            return String.Join(",", typesList.ToArray());
        }
        /// <summary>
        /// Adds the key value pair.
        /// </summary>
        /// <param name="sb">The sb.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="PaymillWrapper.Exceptions.PaymillException"></exception>
        private void addKeyValuePair(StringBuilder sb, string key, object value)
        {
            string reply = "";
            if (value == null) return;
            try
            {
                key = WebUtility.UrlEncode(key.ToLower());

                if (value.GetType().GetTypeInfo().IsEnum)
                {
                    reply = value.ToString().ToLower();
                }
                else if (value.GetType().Equals(typeof(DateTime)))
                {
                    if (value.Equals(DateTime.MinValue))
                    {
                        reply = "";
                    }
                    else
                    {
                        reply = ((DateTime)value).ToUnixTimestamp().ToString();
                    }
                }
                else
                {
                    reply = WebUtility.UrlEncode(value.ToString());
                }

                if (!string.IsNullOrEmpty(reply))
                {
                    if (sb.Length > 0)
                        sb.Append("&");

                    sb.Append(String.Format("{0}={1}", key, reply));
                }

            }
            catch
            {
                throw new PaymillException(
                    String.Format("Unsupported or invalid character set encoding '{0}'.", charset));
            }

        }
        private void encodeFilterParameters(StringBuilder sb, Object filter)
        {
            if (filter != null)
            {
                var props = filter.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                var snakeProps = props.Where(x => x.GetCustomAttributes(typeof(SnakeCase), false).Count() > 0);
                foreach (var prop in snakeProps)
                {
                    object value = prop.GetValue(filter);
                    var snakeProp = (SnakeCase)prop.GetCustomAttributes(typeof(SnakeCase), false).First();
                    if (value != null)
                    {
                        if (value is Boolean)
                        {
                            value = value.ToString().ToLower();
                        }
                        this.addKeyValuePair(sb, snakeProp.Value.ToLower(), value.ToString());
                    }
                }
            }

        }
        private String encodeOrderParameter(Object order)
        {
            String orderEntry = String.Empty;
            String sortEntry = String.Empty;
            if (order != null)
            {
                var props = order.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                var snakeProps = props.Where(x => x.GetCustomAttributes(typeof(SnakeCase), false).Count() > 0);

                foreach (var prop in snakeProps)
                {
                    object value = prop.GetValue(order);
                    var snakeProp = (SnakeCase)prop.GetCustomAttributes(typeof(SnakeCase), false).First();
                    if ((value as Boolean?) == true)
                    {
                        if (snakeProp.Order)
                        {
                            orderEntry += "_" + snakeProp.Value;
                        }
                        else
                        {
                            sortEntry = snakeProp.Value;
                        }
                    }
                }
            }
            return sortEntry + orderEntry;

        }
        public String EncodeFilterParameters(Object filter, Object order, int? count, int? offset)
        {
            StringBuilder sb = new StringBuilder();
            encodeFilterParameters(sb, filter);
            String orderParams = encodeOrderParameter(order);

            if (String.IsNullOrWhiteSpace(orderParams) == false
                    && !orderParams.StartsWith("_"))
            {
                this.addKeyValuePair(sb, "order", orderParams);
            }
            if (count.HasValue && count.Value > 0)
            {
                this.addKeyValuePair(sb, "count", count.Value);
            }
            if (offset.HasValue && offset.Value >= 0)
            {
                this.addKeyValuePair(sb, "offset", offset.Value);
            }
            return sb.ToString();
        }

    }
}
