using Application.Common.Errors;
using Application.Common.Interfaces.Persistence;
using Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Query.Profile;

public class ProfileQueryHandler : IRequestHandler<ProfileQuery, User>
{
    private readonly IUserRepository _userRepository;
    private readonly HttpContext? _context;

    public ProfileQueryHandler(IUserRepository userRepository, IHttpContextAccessor context)
    {
        _userRepository = userRepository;
        _context = context.HttpContext;
    }
    public async Task<User> Handle(ProfileQuery request, CancellationToken cancellationToken)
    {
        User? user = _userRepository.GetUserByEmail(_context.User.Claims.FirstOrDefault(cl => cl.Type == ClaimTypes.Email).Value);
        if (user is null) throw new UserNotFound();
        return user;
    }
}
