using Application.Common.Errors;
using Application.Common.Interfaces.Persistence;
using Domain.Users;
using Domain.Users.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authorization.Handlers;

public class MovieListOwnerAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, MovieList>
{
    private readonly IUserRepository _userRepository;

    public MovieListOwnerAuthorizationHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, MovieList resource)
    {
        if(context.User == null || resource == null) return Task.CompletedTask; 

        //If not asking for Adding, reject
        if(requirement.Name != Constants.AddMovieOperationName && requirement.Name != Constants.GetMoviesofList) 
            return Task.CompletedTask;

        //Get user
        User? user = _userRepository.GetUserByEmail(context.User.Claims.First(cl => cl.Type == ClaimTypes.Email).Value) ?? throw new UserNotFound();

        if (resource.OwnerId == user.Id.Value.ToString())
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}
