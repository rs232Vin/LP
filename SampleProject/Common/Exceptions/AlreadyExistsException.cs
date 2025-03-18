using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Exceptions
{
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException()
        {
        }

        public AlreadyExistsException(string errorMessage) : base(errorMessage)
        {
        }
    }
}
