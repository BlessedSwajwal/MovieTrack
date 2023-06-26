using Domain.Users.Entity;
using Domain.Users.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Persistence;

public interface IStreamedListRepository
{
    void AddStreamedList(StreamedList movieList);

    void AddMovieToList(StreamedListId id, int movieId, int runTime);

    StreamedList GetMovieListById(StreamedListId Id);
}
