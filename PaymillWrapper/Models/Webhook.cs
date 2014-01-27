using Newtonsoft.Json;
using PaymillWrapper.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;


namespace PaymillWrapper.Models
{
   [JsonConverter(typeof(JsonParser<Webhook>))]
    public class Webhook : BaseModel
    {
        [JsonPropertyAttribute("Url")]
        public Uri Url { get; set; }

        [JsonPropertyAttribute("Livemode")]
        public Boolean livemode { get; set; }

         [JsonPropertyAttribute("Email")]
        public String Email { get; set; }

         [JsonPropertyAttribute("Event_Types")]
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
         [JsonIgnore]
         public EventType[] EventTypes { get; set; }
    }
}
