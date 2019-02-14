using System;

namespace Aggregate.Student.Shared
{
    public sealed class StudentMoved : StudentEvent
    {
        public string City { get; }

        public StudentMoved(
            Guid aggregateRootId,
            string city) : base(aggregateRootId)
        {
            City = city;
        }
    }
}