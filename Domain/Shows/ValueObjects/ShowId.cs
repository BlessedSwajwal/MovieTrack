using Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shows.ValueObjects;

public sealed class ShowId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private ShowId(Guid value)
    {
        Value = value;
    }

    public static ShowId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static ShowId Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
