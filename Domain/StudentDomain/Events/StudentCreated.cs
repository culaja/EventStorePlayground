using System;
using System.Collections.Generic;
using Common;

namespace Domain.StudentDomain.Events
{
    public sealed class StudentCreated : AggregateRootCreated
    {
        public Name Name { get; }
        public EmailAddress EmailAddress { get; }
        public Maybe<City> MaybeCity { get; }
        public bool IsHired { get; }

        public StudentCreated(
            Guid aggregateRootId,
            Type aggregateRootType,
            Name name,
            EmailAddress emailAddress,
            Maybe<City> maybeCity,
            bool isHired) : base(aggregateRootId, aggregateRootType)
        {
            Name = name;
            EmailAddress = emailAddress;
            MaybeCity = maybeCity;
            IsHired = isHired;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var item in base.GetEqualityComponents()) yield return item;
            yield return Name;
            yield return EmailAddress;
            yield return MaybeCity;
            yield return IsHired;
        }
    }
}