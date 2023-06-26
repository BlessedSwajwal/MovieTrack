using Application.Common.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Api.Controllers;

//[Route("api/[controller]")]
//[ApiController]

[AllowAnonymous]
public class ErrorsControler : ControllerBase
{
    private readonly ILogger<ErrorsControler> _logger;

    public ErrorsControler(ILogger<ErrorsControler> logger)
    {
        _logger = logger;
    }

    [Route("error")]
    public IActionResult Error()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        _logger.Log(LogLevel.Error, $"{exception!.Message}\n{exception.StackTrace}");

        var (statusCode, message) = exception switch
        {
            IServiceException serviceException => (serviceException.StatusCode, serviceException.ErrorMessage),
            _ => (500, "Unknown error occured.")
        };

        var path = AppDomain.CurrentDomain.BaseDirectory;
        using (StreamWriter sw = new StreamWriter(Path.Combine(path, "errors.txt"), true))
        {
            sw.WriteLine($"{exception.Message}\n\n{exception.StackTrace}\n\n---END---\n");
        }

        return Problem(title: "Error", detail: message, statusCode: statusCode);
    }
}
