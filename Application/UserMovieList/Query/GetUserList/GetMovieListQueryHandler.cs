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

namespace Application.UserMovieList.Query.GetUserList;

public class GetMovieListQueryHandler : IRequestHandler<GetUserMovieListQuery, List<MovieList>>
{
    private readonly IUserRepository _userRepository;
    private readonly HttpContext _httpContext;
    private readonly IMovieListRepository _movieListRepository;

    public GetMovieListQueryHandler(IUserRepository userRepository, IHttpContextAccessor httpContext, IMovieListRepository movieListRepository)
    {
        _userRepository = userRepository;
        _httpContext = httpContext.HttpContext;
        _movieListRepository = movieListRepository;
    }

    async Task<List<MovieList>> IRequestHandler<GetUserMovieListQuery, List<MovieList>>.Handle(GetUserMovieListQuery request, CancellationToken cancellationToken)
    {
        User? user = _userRepository.GetUserByEmail(_httpContext.User.Claims.First(cl => cl.Type == ClaimTypes.Email).Value);

        if (user is null) throw new UserNotFound();

        var movieList = new List<MovieList>();

        foreach (var mvId in user.MovieListIds)
        {
            MovieList? rs = _movieListRepository.GetMovieListById(mvId);
            if (rs is null) continue;
            movieList.Add(rs);
        }

        return movieList;
    }
}
