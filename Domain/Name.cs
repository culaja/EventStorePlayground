using System;
using System.Collections.Generic;
using Common;

namespace Domain
{
    public sealed class Name : ValueObject<Name>
    {
        public string NameAsString { get; }

        public Name(string name)
        {
            NameAsString = name;
        }

        public static Name NameFrom(Maybe<string> maybeName) => maybeName
            .Ensure(m => m.HasValue, () => throw new ArgumentNullException($"{nameof(NameAsString)} can't be empty.", nameof(maybeName)))
            .Ensure(m => !string.IsNullOrWhiteSpace(m.Value), () => throw new ArgumentNullException($"{nameof(NameAsString)} can't be empty.", nameof(maybeName)))
            .Map(name => new Name(name))
            .Value;

        public override string ToString() => NameAsString;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return NameAsString;
        }
    }
}