using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Errors;

public class InvalidVerificationToken : Exception, IServiceException
{
    public int StatusCode => (int)HttpStatusCode.BadRequest;

    public string? ErrorMessage => "The verification token is not valid.";
}
