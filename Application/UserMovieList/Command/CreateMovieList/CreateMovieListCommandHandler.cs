using Application.Common.Errors;
using Application.Common.Interfaces.Persistence;
using Domain.Users;
using Domain.Users.Entity;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserMovieList.Command.CreateMovieList;

public class CreateMovieListCommandHandler : IRequestHandler<CreateMovieListCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IMovieListRepository _movieListRepository;
    private readonly HttpContext _httpContext;

    public CreateMovieListCommandHandler(IMovieListRepository movieListRepository, IHttpContextAccessor httpContext, IUserRepository userRepository)
    {
        _movieListRepository = movieListRepository;
        _httpContext = httpContext.HttpContext;
        _userRepository = userRepository;
    }

    public Task Handle(CreateMovieListCommand request, CancellationToken cancellationToken)
    {
        User? user = _userRepository.GetUserByEmail(_httpContext.User.Claims.FirstOrDefault(cl => cl.Type == ClaimTypes.Email).Value);
        if (user is null) throw new UserNotFound();

        var movieList = MovieList.Create(user.Id.Value.ToString(), request.ListName);

        user.AddNewMovieList(movieList.Id);
        _movieListRepository.AddMovieList(movieList);
        _movieListRepository.Update();

        Console.WriteLine(user);

        return Task.CompletedTask;
    }
}
