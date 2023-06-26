using Application.StreamedContentList.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserMovieList.Query.GetListMovies;

public class MovieListResult
{
    public List<MovieResponse> Results { get; set; } = new();
}
