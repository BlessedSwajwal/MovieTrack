using Application.Authentication.Query.Profile;
using Application.Common.Errors;
using Application.Common.Interfaces.Persistence;
using Contract.Profile;
using Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers;

[Route("profile")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly ISender _mediator;

    public UsersController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("details")]
    public async Task<IActionResult> UserDetails()
    {
        var user = await _mediator.Send(new ProfileQuery());
        return Ok(user);
    }
}
