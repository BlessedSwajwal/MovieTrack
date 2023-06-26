using Application.Common.Errors;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces;
using Application.StreamedContentList.Command.AddMovie;
using Application.StreamedContentList.Common;
using Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.Users.ValueObjects;

namespace Application.StreamedContentList.Query.RemoveMovie;

internal class RemoveMovieQueryHandler : IRequestHandler<RemoveMovieQuery>
{

    private readonly IUserRepository _userRepository;
    private readonly HttpContext _context;
    private readonly IMovieRepository _movieRepository;
    private readonly IStreamedListRepository _streamedListRepository;

    public RemoveMovieQueryHandler(IUserRepository userRepository, IHttpContextAccessor context, IMovieRepository movieRepository, IStreamedListRepository streamedListRepository)
    {
        _userRepository = userRepository;
        _context = context.HttpContext;
        _movieRepository = movieRepository;
        _streamedListRepository = streamedListRepository;
    }

    async Task IRequestHandler<RemoveMovieQuery>.Handle(RemoveMovieQuery request, CancellationToken cancellationToken)
    {

        //First get the movie details.
        var movie = await _movieRepository.GetMovieDetailsAsync(request.TMDB_ID);

        //Get the user
        User? user = _userRepository.GetUserByEmail(_context.User.Claims.FirstOrDefault(cl => cl.Type == ClaimTypes.Email)?.Value) ?? null;

        if (user is null) throw new UserNotFound();

        //Check if the streamedList does not have the content inside.
        var movieListId = user.streamedListId;
        var streamedList = _streamedListRepository.GetMovieListById(movieListId);
        if (!streamedList.MovieIds.Contains(TmdbID.Create(request.TMDB_ID))) throw new NoMovieFound();

        //First add the movie to the user's streamed List
        streamedList.RemoveMovie(TmdbID.Create(movie.TMDB_ID), movie.runTime);

        return;

    }
}
