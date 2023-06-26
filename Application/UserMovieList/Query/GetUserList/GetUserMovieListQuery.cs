using Domain.Users.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserMovieList.Query.GetUserList;

public record GetUserMovieListQuery() : IRequest<List<MovieList>>;
