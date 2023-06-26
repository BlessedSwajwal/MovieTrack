using Application.Authentication.Common;
using Application.Common.Errors;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using BCrypt.Net;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Query.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticationResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginQueryHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthenticationResult> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        //Check if the user exists.
        var user = _userRepository.GetUserByEmail(request.Email);
        if (user is null) throw new InvalidUserDetailsException();

        //Generate JWT token
        var token = _jwtTokenGenerator.GenerateToken(user);

        //Verify the password using Bcrypt.

        if(BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
        {
            return new AuthenticationResult(user, token);
        } else
        {
            throw new InvalidUserDetailsException();
        }

    }
}
