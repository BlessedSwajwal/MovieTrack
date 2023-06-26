using Application.Authorization;
using Application.Common.Errors;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Persistence;
using Domain.Users.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserMovieList.Command.AddMovieToList;

public class AddMovieToListCommandHandler : IRequestHandler<AddMovieToListCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IMovieListRepository _movieListRepository;
    private readonly IAuthorizationService _authorizationService;
    private readonly HttpContext _ctx;
    private readonly IMovieRepository _movieRepository;

    public AddMovieToListCommandHandler(IMovieListRepository movieListRepository, IUserRepository userRepository, IAuthorizationService authorizationService, IHttpContextAccessor ctx, IMovieRepository movieRepository)
    {
        _movieListRepository = movieListRepository;
        _userRepository = userRepository;
        _authorizationService = authorizationService;
        _ctx = ctx.HttpContext;
        _movieRepository = movieRepository;
    }

    public async Task Handle(AddMovieToListCommand request, CancellationToken cancellationToken)
    {
        //Get the movie list.
        var movieList = _movieListRepository.GetMovieListById(request.MovieListId);

        //Authorize
        var isAuthorized = await _authorizationService.AuthorizeAsync(_ctx.User, movieList, MovieListOperations.Add);

        if(!isAuthorized.Succeeded)
        {
            throw new Exception("Not AUthorized.");
        }

        //Check if movie is already in the list
        if(movieList.MovieIds.Contains(TmdbID.Create(request.TMDB_ID)))
        {
            throw new DuplicateMovie();
        }

        //Get movie details
        var movie = await _movieRepository.GetMovieDetailsAsync(request.TMDB_ID);


        _movieListRepository.AddMovieToList(movieList.Id, movie.TMDB_ID, movie.runTime);
   //     _movieListRepository.UpdateMovieList(movieList);

        return;
    }
}
