using Application.Common.Errors;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Persistence;
using Application.StreamedContentList.Common;
using Domain.Users;
using Domain.Users.Entity;
using Domain.Users.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.StreamedContentList.Command.AddMovie;

public class AddMovieCommandHandler : IRequestHandler<AddMovieCommand, MovieResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly HttpContext _context;
    private readonly IMovieRepository _movieRepository;
    private readonly IStreamedListRepository _streamedListRepository;

    public AddMovieCommandHandler(IUserRepository userRepository, IHttpContextAccessor context, IMovieRepository movieRepository, IStreamedListRepository streamedListRepository)
    {
        _userRepository = userRepository;
        _context = context.HttpContext;
        _movieRepository = movieRepository;
        _streamedListRepository = streamedListRepository;
    }

    public async Task<MovieResponse> Handle(AddMovieCommand request, CancellationToken cancellationToken)
    {
        //First get the movie details.
        var movie = await _movieRepository.GetMovieDetailsAsync(request.TMDB_ID);

        //Get the user
        User? user = _userRepository.GetUserByEmail(_context.User.Claims.FirstOrDefault(cl => cl.Type == ClaimTypes.Email)?.Value) ?? null;

        if (user is null) throw new UserNotFound();

        //Check if the streamedList already has the content inside.
        var movieListId = user.streamedListId;
        var streamedList = _streamedListRepository.GetMovieListById(movieListId);
        if (streamedList.MovieIds.Contains(TmdbID.Create(request.TMDB_ID))) throw new DuplicateMovie();

        //First add the movie to the user's streamed List
        // streamedList.AddMovie(TmdbID.Create(movie.TMDB_ID), movie.runTime);
        _streamedListRepository.AddMovieToList(streamedList.Id, movie.TMDB_ID, movie.runTime);
        
        return movie;
    }
}
