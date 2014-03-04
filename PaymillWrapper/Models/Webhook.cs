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
        [DataMember(Name = "id")]
        public String Id { get; set; }

        [DataMember(Name = "url")]
        public Uri Url { get; set; }

        [DataMember(Name = "livemode")]
        public Boolean livemode { get; set; }

         [DataMember(Name = "email")]
        public String Email { get; set; }

        
        /* private String[] CreatedEventTypes
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
          */
         [DataMember(Name = "event_types")]
         public EventType[] EventTypes { get; set; }
    }
}
