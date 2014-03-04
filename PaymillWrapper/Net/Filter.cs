using PaymillWrapper.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PaymillWrapper.Net
{
    public class Filter
    {
        private Encoding charset;
        private Dictionary<string, object> data;

        public Filter()
        {
            charset = Encoding.UTF8;
            data = new Dictionary<string, object>();
        }

        public void Add(string key, object value)
        {
            data.Add(key, value);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string,object> pair in data)
            {
                object value = pair.Value;
                if (value != null)
                    this.addKeyValuePair(sb, pair.Key, value);
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
                    if (value.Equals(DateTime.MinValue)) reply = "";
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
