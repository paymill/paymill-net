using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PaymillWrapper.Models
{
   
    public class Webhook : BaseModel
    {
        [DataMember(Name = "url"),
        Updateable(Name = "url")]
        public Uri Url { get; set; }

        [DataMember(Name = "livemode")]
        public Boolean livemode { get; set; }

        [DataMember(Name = "email"),
        Updateable(Name = "email")]
        public String Email { get; set; }

        [DataMember(Name = "event_types")]
         public EventType[] EventTypes { get; set; }
    }
}
