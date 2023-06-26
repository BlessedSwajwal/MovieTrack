using MediatR;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserMovieList.Command.CreateMovieList;

public record CreateMovieListCommand(
        string ListName
    ) : IRequest;
