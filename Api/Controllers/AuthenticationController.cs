using Application.Authentication.Command.EmailVerification;
using Application.Authentication.Command.Register;
using Application.Authentication.Query.Login;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Contract.Authentication;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers;


//[AllowAnonymous]
[Route("authentication")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public AuthenticationController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpGet("check")]
    public IActionResult Check()
    {
        return Ok("Working correctly.");
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        //Map the RegsterRequest to RegisterCommand
        var command = _mapper.Map<RegisterCommand>(request);

        //Send the RegisterCommand
        var res = await _mediator.Send(command);

        //Map the AuthenticationResult to AuthenticationRespinse
        var registeredUser = res.Adapt<AuthenticationResponse>();

        //Do not need to send any data back.
        return Ok(registeredUser);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        //Map the RegsterRequest to RegisterCommand
        var command = _mapper.Map<LoginQuery>(request);

        //Send the RegisterCommand
        var res = await _mediator.Send(command);

        //Map the AuthenticationResult to AuthenticationRespinse
        var loggedUser = res.Adapt<AuthenticationResponse>();

        //Do not need to send any data back.
        return Ok(loggedUser);
    }

    [AllowAnonymous]
    [HttpGet("verify")]
    public async Task<IActionResult> VerifyEmail([FromQuery]string token, [FromQuery] string email)
    {
        var command = new EmailVerificationCommand(token, email);
        await _mediator.Send(command);
        return Ok();
    }

}
