using System;
using System.Net;

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
