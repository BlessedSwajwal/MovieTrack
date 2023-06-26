using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authorization;

public static class MovieListOperations
{
    public static OperationAuthorizationRequirement Add = new() { Name = Constants.AddMovieOperationName };
}


public class Constants
{
    public static readonly string AddMovieOperationName = "Add";
}