using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymillWrapper.Models
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class Updateable : System.Attribute
    {
        public String Name { get; set; }
        public String OnlyProperty { get; set; }
    }
}
