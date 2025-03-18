using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
        {
            
        }

        public NotFoundException(string errorMessage) : base(errorMessage)
        {
            
        }
    }
}
