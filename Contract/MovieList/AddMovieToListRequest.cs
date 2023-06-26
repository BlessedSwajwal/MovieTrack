using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.MovieList;

public record AddMovieToListRequest(
        int TMDB_ID
    );
