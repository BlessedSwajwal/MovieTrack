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

public class MovieListRepository : IMovieListRepository
{
   // private static readonly List<MovieList> _movieLists = new();
    private readonly TrackItDbContext _dbContext;

    public MovieListRepository(TrackItDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddMovieList(MovieList movieList)
    {
        // _movieLists.Add(movieList);
        _dbContext.Movies.Add(movieList);
    }

    public void Update() { _dbContext.SaveChanges(); }

    public void AddMovieToList(MovieListId id, int movieId, int runTime)
    {
        var movieList = GetMovieListById(id);
        movieList.AddMovie(movieId, runTime);

        _dbContext.SaveChanges();
    }

    public MovieList GetMovieListById(MovieListId id)
    {
       // return _movieLists.Where(mv => mv.Id == id).FirstOrDefault();
        return _dbContext.Movies.Where(mv => mv.Id == id).FirstOrDefault();
    }

    public void UpdateMovieList(MovieList movieList)
    {
        throw new NotImplementedException();
    }
}
