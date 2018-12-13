using System;
using Common;

namespace Domain
{
    public sealed class StudentMoved : DomainEvent<Student>
    {
        public City City { get; }

        public StudentMoved(Guid aggregateRootId, City city) : base(aggregateRootId)
        {
            City = city;
        }
    }
}