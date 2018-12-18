using System;
using System.Collections.Generic;
using Common;

namespace Domain
{
    public sealed class Name : ValueObject<Name>
    {
        private readonly string _name;

        public Name(string name)
        {
            _name = name;
        }

        public static Name NameFrom(Maybe<string> maybeName) => maybeName
            .Ensure(m => m.HasValue, () => throw new ArgumentNullException($"{nameof(Name)} can't be empty.", nameof(maybeName)))
            .Ensure(m => !string.IsNullOrWhiteSpace(m.Value), () => throw new ArgumentNullException($"{nameof(Name)} can't be empty.", nameof(maybeName)))
            .Map(name => new Name(name))
            .Value;

        public override string ToString() => _name;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _name;
        }
    }
}