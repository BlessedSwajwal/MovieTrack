using Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users.ValueObjects;

public class TmdbID : ValueObject
{
    public int Value { get; }

    private TmdbID(int val) 
    {
        Value = val;
    }

    public static TmdbID Create(int val)
    {
        return new(val);
    }


    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private TmdbID() { }
}
