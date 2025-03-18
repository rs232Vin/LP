using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Models.Errors;

namespace WebApi.Utility
{
    public static class CommonUtility
    {
        public static ApplicationError GetApplicationError(string code, string message)
        {
            return new ApplicationError
            {
                Code = code,
                ErrorMessage = message
            };
        }

        public static SystemError GetSystemError()
        {
            return new SystemError
            {
                Code = FaultCodes.LP001,
                ErrorMessage = "System Error"
            };
        }
    }
}