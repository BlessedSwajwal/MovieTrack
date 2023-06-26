using Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Movies.ValueObjects;

public sealed class MovieId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private MovieId(Guid value)
    {
        Value = value;
    }

    public static MovieId CreateUnique()
    {
        return new MovieId(Guid.NewGuid());
    }

    public static MovieId Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private MovieId() { }
}
