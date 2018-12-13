using System;
using Common;

namespace Domain
{
    public sealed class StudentHired : DomainEvent<Student>
    {
        public StudentHired(Guid aggregateRootId) : base(aggregateRootId)
        {
        }
    }
}