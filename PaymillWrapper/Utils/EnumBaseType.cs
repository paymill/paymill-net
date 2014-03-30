using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PaymillWrapper.Utils
{
    public class EnumBaseType
    {
        public String Value { get; private set; }
        private static List<EnumBaseType> createdEnumItems = new List<EnumBaseType>();
        private Boolean unknow;
  
        protected EnumBaseType(String value, Boolean unknowValue = false)
        {
            Value = value;
            unknow = unknowValue;
            createdEnumItems.Add(this);
        }
        public override String ToString()
        {
            return Value;
        }
        public static EnumBaseType GetUnknown(String value, Type t)
        {
            EnumBaseType unknown = EnumBaseType.createdEnumItems.SingleOrDefault(x =>
                         x.GetType() == t
                         && x.unknow == true
                        );
            if (unknown == null)
            {
                return null;
            }
            unknown.Value = value;
            return unknown;
        }
        public static EnumBaseType GetItemByValue(String value, Type t)
        {
            if (EnumBaseType.createdEnumItems.Exists(x=> x.GetType() == t) == false)
            {
                Activator.CreateInstance(t);
            }
            var result = EnumBaseType.createdEnumItems.SingleOrDefault(x => String.Compare(x.Value, value, true) == 0
                    && x.GetType() == t);

            if (result == null)
            {
                result = GetUnknown(value, t);
            }
            return result;
        }
    }
}
