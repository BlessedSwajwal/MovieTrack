using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StreamedContentList.Query.MovieHistory;

public record MovieHistoryQuery() : IRequest<MovieHistoryList>;
