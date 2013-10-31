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
        [DataMember]
         public String Аctive
         {
             get;
             set;
         }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
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
