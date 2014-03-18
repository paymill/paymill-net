using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymillWrapper.Models
{
    public class EventBaseType
    {
        private readonly String name;
        private static List<EventBaseType> createdEvents = new List<EventBaseType>();
        protected EventBaseType(String name)
        {
            this.name = name;
            createdEvents.Add(this);
        }
        public override String ToString()
        {
            return name;
        }
        public static EventBaseType GetEventByName(String name)
        {
            return EventBaseType.createdEvents.Find(x => String.Compare(x.name, name) == 0);
        }
    }
}
