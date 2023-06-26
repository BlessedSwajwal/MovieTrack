using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Errors;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces;
using Domain.Users.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Application.Authorization;

namespace Application.UserMovieList.Query.GetListMovies;

public class GetListMoviesQueryHandler : IRequestHandler<GetListMoviesQuery, MovieListResult>
{
    private readonly HttpContext _httpContext;
    private readonly IUserRepository _userRepository;
    private readonly IMovieListRepository _movieListRepository;
    private readonly IMovieRepository _movieRepository;
    private readonly IAuthorizationService _authorizationService;

    public GetListMoviesQueryHandler(IHttpContextAccessor httpContext, IUserRepository userRepository, IMovieListRepository movieListRepository, IMovieRepository movieRepository, IAuthorizationService authorizationService)
    {
        _httpContext = httpContext.HttpContext;
        _userRepository = userRepository;
        _movieListRepository = movieListRepository;
        _movieRepository = movieRepository;
        _authorizationService = authorizationService;
    }

    public async Task<MovieListResult> Handle(GetListMoviesQuery request, CancellationToken cancellationToken)
    {
        var user = _userRepository.GetUserByEmail(_httpContext.User.Claims.FirstOrDefault(cl => cl.Type == ClaimTypes.Email).Value);

        if (user is null) throw new InvalidUserDetailsException();

        var movieList = _movieListRepository.GetMovieListById(MovieListId.Create(request.listId));

        var isAuthorized = await _authorizationService.AuthorizeAsync(_httpContext.User, movieList, MovieListOperations.GetMovies);

        if (!isAuthorized.Succeeded) throw new Exception("Not Authorized.");

        var movieListResult = new MovieListResult();

        foreach(var movie in movieList.MovieIds) {
            var mv = await _movieRepository.GetMovieDetailsAsync(movie.Value);
            if (mv is null) continue;
            movieListResult.Results.Add(mv);
        }

        return movieListResult;
    }
}
