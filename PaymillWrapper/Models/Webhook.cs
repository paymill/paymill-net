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
        [DataMember(Name = "Id")]
        public String Id { get; set; }

        [DataMember(Name = "Url")]
        public Uri Url { get; set; }

        [DataMember(Name = "Livemode")]
        public Boolean livemode { get; set; }

         [DataMember(Name = "Email")]
        public String Email { get; set; }

         [DataMember(Name = "Event_Types")]
         private String[] Created_EventTypes
         {
             get
             {
                 return null;
             }
             set {
                 if (value != null)
                 {
                     List<EventType> eventsList = new List<EventType>();
                     foreach (String eventName in value)
                     {
                         eventsList.Add(EventType.GetEventByName(eventName));
                     }
                     this.EventTypes = eventsList.ToArray();
                 }
             }
         }
         public EventType[] EventTypes { get; set; }
    }
}
