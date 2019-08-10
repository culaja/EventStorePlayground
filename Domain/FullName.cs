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

        public static FullName FullNameFrom(string fullName) =>
            new FullName(fullName);
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _fullName;
        }

        public override string ToString() => _fullName;

        public static implicit operator string(FullName fullName) => fullName.ToString();
    }
}