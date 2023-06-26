using Application.StreamedContentList.Command.AddMovie;
using Application.StreamedContentList.Query.MovieHistory;
using Application.StreamedContentList.Query.RemoveMovie;
using Contract.AddMovie;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace Api.Controllers;

[Route("streamed")]
[ApiController]
public class StreamedListController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly IMapper _mapster;

    public StreamedListController(ISender mediator, IMapper mapster)
    {
        _mediator = mediator;
        _mapster = mapster;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddMovie(AddMovieRequest request)
    {
        var command = new AddMovieCommand(request.TMDB_ID);
       var movie = await _mediator.Send(command);
        return Ok(movie);
    }

    [HttpPost("remove")]
    public async Task<IActionResult> RemoveMovie(AddMovieRequest request)
    {
        var command = new RemoveMovieQuery(request.TMDB_ID);
        await _mediator.Send(command);
        return Ok();
    }

    [HttpGet("MovieHistory")]
    public async Task<IActionResult> GetWatchedMovies()
    {
        var query = new MovieHistoryQuery();
        var movieHistoryList = await _mediator.Send(query);
        return Ok(movieHistoryList);
    }
}
