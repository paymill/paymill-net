using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Web;

namespace PaymillWrapper
{
     public class SubscriptionCount
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyAttribute("Аctive")]
         public String Аctive
         {
             get;
             set;
         }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyAttribute("Inactive")]
        public string Inactive
        {
            get;
            set;
        }

        public SubscriptionCount(String active, String inactive)
        {
            Аctive = active;
            Inactive = inactive;
        }
    }
}
