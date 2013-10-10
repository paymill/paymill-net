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
        /// <summary>
        /// Unique identifier
        /// </summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Creation date
        /// </summary>
        [DataMember(Name = "created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Last update
        /// </summary>
        [DataMember(Name = "updated_at")]
        public DateTime UpdatedAt { get; set; }
        /*
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

        [DataMember(Name = "created_at")]
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

        [DataMember(Name = "updated_at")]
        public int UpdatedAtTicks
        {
            get { return (int)(this.Updated_At - unixEpoch).TotalSeconds; }
            set { this.Updated_At = unixEpoch.AddSeconds(value); }
        }
        */
    }
}
