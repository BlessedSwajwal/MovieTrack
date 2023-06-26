using Domain.Common.Models;
using Domain.Movies.Entity;
using Domain.Movies.ValueObjects;

namespace Domain.Movies;

public sealed class Movie : AggregateRoot<MovieId, Guid>
{
    public string ImdbId { get; }
    public string TmdbId { get; }
    public string Name { get; }
    public string Description { get; }
    private List<Genre> _genres = new();
    public IReadOnlyList<Genre> Genres => _genres.AsReadOnly();
    public string Backdroppath { get; }
    public string Posterpath { get; }
    public int Runtime { get; }

    public float Rating { get; }
    public float VoteCount { get; }

    private Movie(
        MovieId id,
        string imdbId,
        string tmdbId,
        string name,
        string description,
        string backdroppath,
        int runtime,
        float rating,
        float voteCount,
        List<Genre> genres,
        string posterpath)
        : base(id)
    {
        ImdbId = imdbId;
        TmdbId = tmdbId;
        Name = name;
        Description = description;
        Backdroppath = backdroppath;
        Runtime = runtime;
        Rating = rating;
        VoteCount = voteCount;
        _genres = genres;
        Posterpath = posterpath;
    }

    public static Movie Create(
        string imdbId, string tmdbId, string name, string description, string backdroppath, int runtime, float rating, float voteCount, List<Genre> genres, string posterPath)
    {
        return new(
                MovieId.CreateUnique(),
                imdbId,
                tmdbId,
                name,
                description,
                backdroppath,
                runtime,
                rating,
                voteCount,
                genres, 
                posterPath
            );
    }

}
