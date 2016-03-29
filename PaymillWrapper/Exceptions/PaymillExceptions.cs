using System;

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
