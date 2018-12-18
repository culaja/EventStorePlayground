using System;
using Common;
using Common.Eventing;

namespace Domain.StudentDomain.Events
{
    public sealed class StudentHired : DomainEvent<Student>
    {
        public StudentHired(Guid aggregateRootId) : base(aggregateRootId)
        {
        }
    }
}