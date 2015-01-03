using Newtonsoft.Json;
using PaymillWrapper.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PaymillWrapper.Models
{
    [JsonConverter(typeof(StringToBaseModelConverter<Merchant>))]
    public class Merchant : BaseModel
    {
        public Merchant()
        {
        }

        public Merchant(String identifier)
        {
            Identifier = identifier;
        }
        /**
       * unique identifier of this merchant.
       */
        [DataMember(Name = "identifier_key")]
        public String Identifier { get; set; }
        /**
        * email address of this merchant.
        */
         [DataMember(Name = "email")]
        public String Email { get; set; }

        /**
         * culture setting of this merchant.
         */
         [DataMember(Name = "locale")]
        public String Locale { get; set; }

        /**
         * country code of this merchant.
         */
        [DataMember(Name = "country")]
        public String Country { get; set; }

        /**
         * List of activated card brands of this merchant.
         */
         [DataMember(Name = "methods")]
        public List<String> Methods { get; set; }
    }
}
