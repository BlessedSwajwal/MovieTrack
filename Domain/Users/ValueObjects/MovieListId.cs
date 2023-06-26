using Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users.ValueObjects;

public sealed class MovieListId : ValueObject
{
        public Guid Value { get; }

        private MovieListId(Guid value)
        {
            Value = value;
        }

        public static MovieListId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public static MovieListId Create(Guid value)
        {
            return new(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }

