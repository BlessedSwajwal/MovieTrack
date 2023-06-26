using Application.Common.Interfaces.Persistence;
using Domain.Users.Entity;
using Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Persistence;

public class StreamedListRepository : IStreamedListRepository
{
    // private static readonly List<StreamedList> _movieLists = new();
    private readonly TrackItDbContext _dbContext;

    public StreamedListRepository(TrackItDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddStreamedList(StreamedList streamedList)
    {
        //  _movieLists.Add(streamedList);
        _dbContext.StreamedLists.Add(streamedList);
        _dbContext.SaveChanges();

    }

    public void AddMovieToList(StreamedListId id, int movieId, int runTime)
    {
        var streamedList = _dbContext.StreamedLists.Where(x => x.Id == id).FirstOrDefault();
        if (streamedList is null) throw new Exception("Invalid Streamlist");

        streamedList.AddMovie(movieId, runTime);
        _dbContext.SaveChanges();
    }

    public StreamedList GetMovieListById(StreamedListId id)
    {
        return _dbContext.StreamedLists.Where(mv => mv.Id == id).FirstOrDefault();
    }
}
