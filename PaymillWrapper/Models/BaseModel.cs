using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PaymillWrapper.Models
{
    [DataContract]
    public class BaseModel
    {
        private static readonly DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).ToLocalTime();

        /// <summary>
        /// Unique identifier
        /// </summary>
        [DataMember(Name = "Id")]
        public string Id { get; set; }

        /// <summary>
        /// Creation date
        /// </summary>
        [IgnoreDataMember]
        public DateTime Created_At { get; set; }

        [DataMember(Name = "Created_At")]
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
        [IgnoreDataMember]
        public DateTime Updated_At { get; set; }

        [DataMember(Name = "Updated_At")]
        public int Updated_At_Ticks
        {
            get { return (int)(this.Updated_At - unixEpoch).TotalSeconds; }
            set { this.Updated_At = unixEpoch.AddSeconds(value); }
        }
    }
}
