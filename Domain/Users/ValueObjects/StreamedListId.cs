using Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users.ValueObjects;

public sealed class StreamedListId : ValueObject
{
    public Guid Value { get; }

    private StreamedListId(Guid value)
    {
        Value = value;
    }

    public static StreamedListId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static StreamedListId Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private StreamedListId() { }
}
