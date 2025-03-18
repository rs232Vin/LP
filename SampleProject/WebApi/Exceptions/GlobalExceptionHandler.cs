using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;
using System.Web.Mvc;
using WebApi.Models;
using WebApi.Models.Errors;
using WebApi.Utility;

namespace WebApi.Exceptions
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {           
            // Log the exception.

            var result = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("Internal Server Error Occurred."),
                ReasonPhrase = "Exception"
            };

            context.Result = new ErrorMessageResult(context.Request, result);
        }
    }
}