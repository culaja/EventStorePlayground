using System;
using Common;
using Shared.Common;

namespace Aggregate.Student.Shared
{
    public sealed class StudentCreated : StudentEvent, IAggregateRootCreated
    {
        public string Name { get; }
        public string EmailAddress { get; }
        public Maybe<string> MaybeCity { get; }
        public bool IsHired { get; }

        public StudentCreated(
            Guid aggregateRootId,
            string name,
            string emailAddress,
            Maybe<string> maybeCity,
            bool isHired) : base(aggregateRootId)
        {
            Name = name;
            EmailAddress = emailAddress;
            MaybeCity = maybeCity;
            IsHired = isHired;
        }
    }
}