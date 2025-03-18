using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Exceptions
{
    public class ValidationException : ArgumentException
    {
        public ValidationException()
        {
        }

        public ValidationException(string errorMessage) : base(errorMessage)
        {

        }
    }
}
