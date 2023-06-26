using Domain.Users.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserMovieList.Command.AddMovieToList;

public record AddMovieToListCommand(
    MovieListId MovieListId,
    int TMDB_ID
 ) : IRequest;
