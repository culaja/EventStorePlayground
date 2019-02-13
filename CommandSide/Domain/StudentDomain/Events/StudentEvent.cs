using System;
using Common.Messaging;

namespace Domain.StudentDomain.Events
{
    public abstract class StudentEvent : DomainEvent<Student>
    {
        protected StudentEvent(Guid aggregateRootId) : base(aggregateRootId)
        {
        }
    }
}