using System;
using Common.Messaging;

namespace Domain.StudentDomain.Events
{
    public sealed class StudentHired : DomainEvent<Student>
    {
        public StudentHired(Guid aggregateRootId) : base(aggregateRootId)
        {
        }
    }
}