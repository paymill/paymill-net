using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymillWrapper
{
    public class PaymillException : Exception
    {
        public PaymillException(string mensaje):base(mensaje)
        {
        }
    }
}