using Application.StreamedContentList.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StreamedContentList.Query.RemoveMovie;

public record RemoveMovieQuery(
   int TMDB_ID
) : IRequest;
