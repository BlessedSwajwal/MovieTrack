using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserMovieList.Query.GetListMovies;

public record GetListMoviesQuery
(
    Guid listId
) : IRequest<MovieListResult> ;
