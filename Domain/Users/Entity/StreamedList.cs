using Domain.Common.Models;
using Domain.Movies.ValueObjects;
using Domain.Users.ValueObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users.Entity;

public sealed class StreamedList : Entity<StreamedListId>
{
    private readonly List<TmdbID> _movieIds = new List<TmdbID>();
    public IReadOnlyList<TmdbID> MovieIds => _movieIds.AsReadOnly();
    public int ListMinutes { get; private set; } = 0;

    private StreamedList(StreamedListId Id) : base(Id)
    {}

    public static StreamedList Create()
    {
        return new(
            StreamedListId.CreateUnique()
           );
    }

    public void AddMovie(int movieId, int runTime)
    {
        _movieIds.Add(TmdbID.Create(movieId));
        ListMinutes += runTime;
    }

    public void RemoveMovie(TmdbID movieId, int runTime) {
        _movieIds.Remove(movieId);
        ListMinutes -= runTime;
    }

    public bool HasMovie(TmdbID movieId)
    {
        return _movieIds.Contains(movieId);
    }

    private StreamedList() { }
}

