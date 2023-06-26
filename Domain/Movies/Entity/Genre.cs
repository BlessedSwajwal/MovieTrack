using Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Movies.Entity;

public sealed class Genre : Entity<int>
{
    public int Name { get; }
    private Genre(int id, int name) : base(id)
    {
        Name = name;
    }

    public static Genre Create(int id, int name)
    {
        return new Genre(id, name);
    }
}
