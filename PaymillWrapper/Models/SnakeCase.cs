using System;

namespace PaymillWrapper.Models
{
    [System.AttributeUsage(System.AttributeTargets.Property | System.AttributeTargets.Field)]
    public class SnakeCase : System.Attribute
    {
        public String Value { get; set; }
        public Boolean Order { get; set; }
    }
}
