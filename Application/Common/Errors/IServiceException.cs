using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Errors;

public interface IServiceException
{
    public int StatusCode { get; }
    public string? ErrorMessage { get; }
}
