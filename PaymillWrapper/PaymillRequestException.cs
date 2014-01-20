﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;


namespace PaymillWrapper
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
