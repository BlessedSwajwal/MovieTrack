using Application.Common.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

//[Route("api/[controller]")]
//[ApiController]

[AllowAnonymous]
public class ErrorsControler : ControllerBase
{
    [Route("error")]
    public IActionResult Error()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        var (statusCode, message) = exception switch
        {
            IServiceException serviceException => (serviceException.StatusCode, serviceException.ErrorMessage),
            _ => (500, "Unknown error occured.")
        };
       
        return Problem(title: "Error", detail: message, statusCode: statusCode);
    }
}
