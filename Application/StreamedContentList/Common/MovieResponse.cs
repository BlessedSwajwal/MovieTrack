using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StreamedContentList.Common;

public record MovieResponse(
    int TMDB_ID,
    string name,
    string description,
    int runTime
);
