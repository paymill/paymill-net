using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PaymillWrapper.Exceptions
{
    public class PaymillRequestException : Exception
    {
        HttpStatusCode _exceptionCode;
        public HttpStatusCode ExceptionCode { get { return _exceptionCode; } }
        public PaymillRequestException(string message, HttpStatusCode exceptionCode)
            : base(message)
        {
            _exceptionCode = exceptionCode;
        }
    }
}
