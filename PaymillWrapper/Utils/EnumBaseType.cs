using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymillWrapper.Utils
{
    public class EnumBaseType
    {
        private readonly String name;
        private static List<EnumBaseType> createdEvents = new List<EnumBaseType>();
        protected EnumBaseType(String name)
        {
            this.name = name;
            createdEvents.Add(this);
        }
        public override String ToString()
        {
            return name;
        }
        public static EnumBaseType GetEventByName(String name)
        {
            return EnumBaseType.createdEvents.Find(x => String.Compare(x.name, name) == 0);
        }
    }
}
