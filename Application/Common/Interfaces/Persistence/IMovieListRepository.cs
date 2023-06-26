using Domain.Users.Entity;
using Domain.Users.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Persistence;

public interface IMovieListRepository
{
    void AddMovieList(MovieList movieList);
    void Update();
    void AddMovieToList(MovieListId id, int movieId, int runTime);
    void UpdateMovieList(MovieList movieList);
    MovieList GetMovieListById(MovieListId Id);
}
