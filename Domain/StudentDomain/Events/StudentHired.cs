using System;

namespace Domain.StudentDomain.Events
{
    public sealed class StudentHired : StudentEvent
    {
        public StudentHired(Guid aggregateRootId) : base(aggregateRootId)
        {
        }
    }
}