﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Errors;

public class InvalidUserDetailsException : Exception, IServiceException
{
    public int StatusCode => (int)HttpStatusCode.NotFound;

    public string? ErrorMessage => "No such user exists.";
}
