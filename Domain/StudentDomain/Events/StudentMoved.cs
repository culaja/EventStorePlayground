using System;
using Common.Messaging;

namespace Domain.StudentDomain.Events
{
    public sealed class StudentMoved : StudentEvent
    {
        public City City { get; }

        public StudentMoved(
            Guid aggregateRootId,
            ulong version,
            City city) : base(aggregateRootId, version)
        {
            City = city;
        }
    }
}