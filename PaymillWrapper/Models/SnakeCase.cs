using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymillWrapper.Models
{
    [System.AttributeUsage(System.AttributeTargets.Property | System.AttributeTargets.Field)]
    public class SnakeCase : System.Attribute
    {
        public String Value { get; set; }
        public Boolean Order { get; set; }
    }
}
