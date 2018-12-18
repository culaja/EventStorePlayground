using System;
using Common;
using Common.Eventing;

namespace Domain.StudentDomain.Events
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