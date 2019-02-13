using System;

namespace Domain.StudentDomain.Events
{
    public sealed class StudentMoved : StudentEvent
    {
        public City City { get; }

        public StudentMoved(
            Guid aggregateRootId,
            City city) : base(aggregateRootId)
        {
            City = city;
        }
    }
}