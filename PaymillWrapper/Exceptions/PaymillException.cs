using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymillWrapper.Exceptions
{
    public class PaymillException : Exception
    {
        public PaymillException(string message)
            : base(message)
        {
            
        }
    }
   
}