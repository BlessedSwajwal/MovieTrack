using Domain.Common.Models;
using Domain.Movies.ValueObjects;
using Domain.Movies;
using Domain.Shows.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Movies.Entity;

namespace Domain.Shows;

public sealed class Show : AggregateRoot<ShowId, Guid>
{
    private Show(ShowId id, string imdbId, string tmdbId, string name, string description, List<Genre> genres, string backdroppath, string posterpath, float rating, float voteCount, int noOfSeasons, int noOfEpisodes) : base(id)
    {
        ImdbId = imdbId;
        TmdbId = tmdbId;
        Name = name;
        Description = description;
        _genres = genres;
        Backdroppath = backdroppath;
        Posterpath = posterpath;
        Rating = rating;
        VoteCount = voteCount;
        NoOfSeasons = noOfSeasons;
        NoOfEpisodes = noOfEpisodes;
    }

    public string ImdbId { get; }
    public string TmdbId { get; }
    public string Name { get; }
    public string Description { get; }
    private readonly List<Genre> _genres = new();

    public IReadOnlyList<Genre> Genres => _genres.AsReadOnly();

    public string Backdroppath { get; }
    public string Posterpath { get; }
    public float Rating { get; }
    public float VoteCount { get; }
    public int NoOfSeasons { get; }
    public int NoOfEpisodes { get; }
    

    public static Show Create(string imdbId, string tmdbId, string name, string description, List<Genre> genres, string backdroppath, string posterpath, float rating, float voteCount, int noOfSeasons, int noOfEpisodes)
    {
        return new(
            ShowId.CreateUnique(),
            imdbId,
            tmdbId,
            name,
            description,
            genres,
            backdroppath,
            posterpath,
            rating,
            voteCount,
            noOfSeasons,
            noOfEpisodes
            );
    }
}
