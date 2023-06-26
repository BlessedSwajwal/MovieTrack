using Application.StreamedContentList.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StreamedContentList.Query.MovieHistory;

public class MovieHistoryList
{
    public List<MovieResponse> MovieHistory { get; set; } = new();
}
