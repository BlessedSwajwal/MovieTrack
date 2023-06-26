using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Errors;

public class NoMovieFound : Exception, IServiceException
{
    public int StatusCode => (int)HttpStatusCode.PreconditionRequired;

    public string? ErrorMessage => "Movie needs to be in the list.";
}
