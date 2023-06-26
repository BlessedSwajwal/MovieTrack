using Application.Authentication.Common;
using Application.Common.Errors;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Service;
using BCrypt.Net;
using Domain.Users;
using Domain.Users.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Command.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthenticationResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IEmailService _emailService;
    private readonly IGenerateToken _generateToken;
    private readonly IStreamedListRepository _streamedListRepository;

    public RegisterCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator, IEmailService emailService, IGenerateToken generateToken, IStreamedListRepository streamedListRepository)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _emailService = emailService;
        _generateToken = generateToken;
        _streamedListRepository = streamedListRepository;
    }

    public async Task<AuthenticationResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        //Check if User already exists
        if (_userRepository.GetUserByEmail(request.Email) is not null) throw new DuplicateEmailException();

        //Create the streamedList for the new user

        var streamedList = StreamedList.Create();
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

        //Create the user
        var user = User.Create(request.FirstName, request.LastName, request.Email, hashedPassword, streamedList);

        //Save the user
        _userRepository.Add(user);
        //Save the users streamed List
        _streamedListRepository.AddStreamedList(streamedList);

        var token = _jwtTokenGenerator.GenerateToken(user);

        
        var verificationToken = _generateToken.GetToken(user.Id.Value.ToString());

        
        _emailService.SendVerificationEmailAsync(user.Email, verificationToken);

        return new AuthenticationResult(user, token);
    }
}
