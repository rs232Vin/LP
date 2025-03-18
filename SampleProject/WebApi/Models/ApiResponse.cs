using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        //public string ErrorMessage { get; set; }
        public IList<Errors.ApplicationError> ApplicationErrors { get; set; }
        public IList<Errors.SystemError> SystemErrors { get; set; }
        public ApiResponse()
        {
        }
    }
}