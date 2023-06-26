using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Errors;

public class DuplicateMovie : Exception, IServiceException
{
    public int StatusCode => (int)HttpStatusCode.Conflict;

    public string? ErrorMessage => "Movie is already on the list";
}
