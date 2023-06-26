using Application.Common.Errors;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Command.EmailVerification;

public class EmailVerificationCommandHandler : IRequestHandler<EmailVerificationCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IGenerateToken _generateToken;

    public EmailVerificationCommandHandler(IUserRepository userRepository, IGenerateToken generateToken)
    {
        _userRepository = userRepository;
        _generateToken = generateToken;
    }

    public Task Handle(EmailVerificationCommand request, CancellationToken cancellationToken)
    {
        var user = _userRepository.GetUserByEmail(request.email);
        string? realToken = _generateToken.GetToken($"{user.Id.Value}");
        if (realToken != request.token)
        {
            throw new InvalidVerificationToken();
        }
        user.VerifyEmail();
        _userRepository.SaveChanges();

        return Task.CompletedTask;
    }
}
