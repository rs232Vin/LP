﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models.Errors
{
    public class SystemError
    {
        public string Code { get; set; }
        public string ErrorMessage { get; set; }
    }
}