using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Errors;
using Domain.Users;
using System.Security.Claims;

namespace Application.StreamedContentList.Query.MovieHistory;

public class MovieHistoryQueryHandler : IRequestHandler<MovieHistoryQuery, MovieHistoryList>
{
    private readonly IUserRepository _userRepository;
    private readonly HttpContext _context;
    private readonly IMovieRepository _movieRepository;
    private readonly IStreamedListRepository _streamedListRepository;

    public MovieHistoryQueryHandler(IUserRepository userRepository, IHttpContextAccessor context, IMovieRepository movieRepository, IStreamedListRepository streamedListRepository)
    {
        _userRepository = userRepository;
        _context = context.HttpContext;
        _movieRepository = movieRepository;
        _streamedListRepository = streamedListRepository;
    }

    async Task<MovieHistoryList> IRequestHandler<MovieHistoryQuery, MovieHistoryList>.Handle(MovieHistoryQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        //Get the user
        User? user = _userRepository.GetUserByEmail(_context.User.Claims.FirstOrDefault(cl => cl.Type == ClaimTypes.Email)?.Value) ?? null;

        //If user is not found

        if (user is null) throw new UserNotFound();

        var movieListId = user.streamedListId;
        var streamedList = _streamedListRepository.GetMovieListById(movieListId);

        var movieHistoryList = new MovieHistoryList();

        //Get the movie details 
        foreach(var movie in streamedList.MovieIds) { 
            var movieData = await _movieRepository.GetMovieDetailsAsync(movie.Value);
            movieHistoryList.MovieHistory.Add(movieData);
        }

        return movieHistoryList;
    }
}
