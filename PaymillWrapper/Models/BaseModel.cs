using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;


namespace PaymillWrapper.Models
{
    [DataContract]
    public class BaseModel
    {
        private static readonly DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).ToLocalTime();

        /// <summary>
        /// Unique identifier
        /// </summary>
        [JsonPropertyAttribute("Id")]
        public string Id { get; set; }

        /// <summary>
        /// Creation date
        /// </summary>
        [JsonIgnore]
        public DateTime Created_At { get; set; }

        [JsonPropertyAttribute("Created_At")]
        public int Created_At_Ticks
        {
            get { 
                return (int)(this.Created_At - unixEpoch).TotalSeconds; 
            }
            set { this.Created_At = unixEpoch.AddSeconds(value); }
        }

        /// <summary>
        /// Last update
        /// </summary>
        [JsonIgnore]
        public DateTime Updated_At { get; set; }

        [JsonPropertyAttribute("Updated_At")]
        public int Updated_At_Ticks
        {
            get { return (int)(this.Updated_At - unixEpoch).TotalSeconds; }
            set { this.Updated_At = unixEpoch.AddSeconds(value); }
        }
    }
}
