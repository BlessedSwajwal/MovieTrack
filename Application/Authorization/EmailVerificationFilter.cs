using Application.Common.Errors;
using Application.Common.Interfaces.Persistence;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authorization;

public class EmailVerifiedHandler : AuthorizationHandler<EmailVerifiedRequirement>
{
    private readonly IUserRepository _userRepository;
    private readonly HttpContext _context;

    public EmailVerifiedHandler(IUserRepository userRepository, IHttpContextAccessor contextAccessor)
    {
        _userRepository = userRepository;
        _context = contextAccessor.HttpContext;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EmailVerifiedRequirement requirement)
    {

        //Get the user
        User? user = _userRepository.GetUserByEmail(_context.User.Claims.FirstOrDefault(cl => cl.Type == ClaimTypes.Email).Value);

        if (user is null) throw new UserNotFound();

        if(user.EmailVerified)
        {
            context.Succeed(requirement);
        } else
        {
            context.Fail();
        }
        return Task.CompletedTask;
    }
}
