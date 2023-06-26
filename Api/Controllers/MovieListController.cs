using Application.UserMovieList.Command.AddMovieToList;
using Application.UserMovieList.Command.CreateMovieList;
using Application.UserMovieList.Query.GetListMovies;
using Application.UserMovieList.Query.GetUserList;
using Contract.MovieList;
using Domain.Users.ValueObjects;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("MovieList")]
[ApiController]
public class MovieListController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly IMapper _mapster;

    public MovieListController(ISender mediator, IMapper mapster)
    {
        _mediator = mediator;
        _mapster = mapster;
    }

    [HttpGet]
    public async Task<IActionResult> GetMovieListAsync()
    {
        var query = new GetUserMovieListQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost("create")] 
    public async Task<IActionResult> CreateMovieList(CreateMovieListRequest request)
    {
        var command = _mapster.Map<CreateMovieListRequest, CreateMovieListCommand>(request);
        await _mediator.Send(command);
        return Ok();
    }

    [HttpPost("{listId}/AddMovie")]
    public async Task<IActionResult> AddMovieToList([FromRoute]Guid listId, AddMovieToListRequest request)
    {
        var movieListId = MovieListId.Create(listId);
        var command = new AddMovieToListCommand(movieListId, request.TMDB_ID);
        await _mediator.Send(command);
        return Ok();
    }

    [HttpGet("{listId}/GetMovies")]
    public async Task<IActionResult> GetListMovies([FromRoute]Guid listId)
    {
        var query = new GetListMoviesQuery(listId);
        var res = await _mediator.Send(query);
        return Ok(res);
    }
}
