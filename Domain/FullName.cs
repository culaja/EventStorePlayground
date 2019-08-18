using System.Collections.Generic;
using Common;

namespace Domain
{
    public sealed class FullName : ValueObject<FullName>
    {
        private readonly string _fullName;

        private FullName(string fullName)
        {
            _fullName = fullName;
        }

        public static FullName FullNameFrom(Maybe<string> maybeFullName) => maybeFullName
            .IfNullOrWhitespace(() => throw new InvalidFullNameException($"{nameof(FullName)} can't be null, empty or white space string."))
            .Map(fullName => new FullName(fullName)).Value;
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _fullName;
        }

        public override string ToString() => _fullName;

        public static implicit operator string(FullName fullName) => fullName.ToString();
    }
}