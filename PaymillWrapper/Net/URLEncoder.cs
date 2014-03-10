using PaymillWrapper.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using PaymillWrapper.Models;
namespace PaymillWrapper.Net
{
    public class UrlEncoder
    {
        private Encoding charset;

        public UrlEncoder()
        {
            charset = Encoding.UTF8;
        }

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
        public string EncodeObject(Object data)
        {
            var props = data.GetType().GetProperties();
            StringBuilder sb = new StringBuilder();
            foreach (var prop in props)
            {
                object value = prop.GetValue(data, null);
                if (value != null)
                    this.addKeyValuePair(sb, prop.Name.ToLower(), value);
            }

            return sb.ToString();
        }
        public string EncodeUpdate(Object data)
        {
            var props = data.GetType().GetProperties();
            StringBuilder sb = new StringBuilder();
            var updatebles = props.Where(x => x.GetCustomAttributes(typeof(Updateable), false).Length > 0);
            foreach (var prop in updatebles)
            {
                object value = prop.GetValue(data, null);
                var updateProps = (Updateable)prop.GetCustomAttributes(typeof(Updateable), false).First();
                if (updateProps.OnlyProperty != null)
                {
                    var valueProp = value.GetType().GetProperty(updateProps.OnlyProperty);
                    value = valueProp.GetValue(value, null);
                }
                if (value != null)
                {
                    this.addKeyValuePair(sb, updateProps.Name.ToLower(), value.ToString());
                }
            }

            return sb.ToString();
        }
        private void addKeyValuePair(StringBuilder sb, string key, object value)
        {
            string reply = "";
            if (value == null) return;
            try
            {
                key = HttpUtility.UrlEncode(key.ToLower(), this.charset);

                if (value.GetType().IsEnum)
                {
                    reply = value.ToString().ToLower();
                }
                else if (value.GetType().Equals(typeof(DateTime)))
                {
                    if (value.Equals(DateTime.MinValue)) reply="";
                }
                else
                {
                    reply = HttpUtility.UrlEncode(value.ToString(), this.charset);
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
    }
}