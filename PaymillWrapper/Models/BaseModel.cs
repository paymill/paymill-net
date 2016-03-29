using Newtonsoft.Json;
using PaymillWrapper.Utils;
using System;

namespace PaymillWrapper.Models
{
    public class BaseModel
    {

        /// <summary>
        /// Unique identifier
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Creation date
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Last update
        /// </summary>
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        public class BaseFilter
        {
            [SnakeCase(Value = "created_at")]
            protected String createdAt;

            [SnakeCase(Value = "updated_at")]
            protected String updatedAt;

            protected void byCreatedAt(DateTime startCreatedAt, DateTime endCreatedAt)
            {
                this.createdAt = String.Format("{0}-{1}", startCreatedAt.ToUnixTimestamp(), endCreatedAt.ToUnixTimestamp());
            }

            protected void byUpdatedAt(DateTime startUpdatedAt, DateTime endUpdatedAt)
            {
                this.updatedAt = String.Format("{0}-{1}", startUpdatedAt.ToUnixTimestamp(), endUpdatedAt.ToUnixTimestamp());
            }
        }
        public class BaseOrder
        {
            [SnakeCase(Value = "asc", Order = true)]
            protected Boolean asc;

            [SnakeCase(Value = "desc", Order = true)]
            protected Boolean desc;

            protected void setAsc()
            {
                this.asc = true;
                this.desc = false;
            }

            protected void setDesc()
            {
                this.asc = false;
                this.desc = true;
            }
        }
    }
}
