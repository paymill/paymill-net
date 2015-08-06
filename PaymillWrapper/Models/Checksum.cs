using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using PaymillWrapper.Utils;

namespace PaymillWrapper.Models
{
    /// <summary>
    /// Checksum validation is a simple method to ensure the integrity of transferred data. Basically, we generate a hash out of the
    /// given parameters and your private API key. If you send us a request with transaction data and the generated checksum, we can
    /// easily validate the data. To make the checksum computation as easy as possible we provide this endpoint for you.
    /// For transactions that are started client-side, e.g. PayPal checkout, it is required to first create a checksum on your server
    /// and then provide that checksum when starting the transaction in the browser. The checksum needs to contain all data required to
    /// subsequently create the actual transaction.
    /// </summary>
    [JsonConverter(typeof(StringToBaseModelConverter<Checksum>))]
    public class Checksum : BaseModel
    {
        [DataMember(Name = "type")]
        public String Type;

        [DataMember(Name = "checksum")]
        public String Value;

        [DataMember(Name = "data")]
        public String Data;
        public Checksum()
        {
        }
        public Checksum(String id)
        {
            Id = id;
        }

        [DataMember(Name = "app_id")]
        public String AppId;

    }


}