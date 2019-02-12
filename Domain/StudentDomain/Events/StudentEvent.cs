using System;
using Common.Messaging;

namespace Domain.StudentDomain.Events
{
    public abstract class StudentEvent : DomainEvent<Student>
    {
        protected StudentEvent(Guid aggregateRootId, ulong version) : base(aggregateRootId, version)
        {
        }
    }
}