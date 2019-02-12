using System;

namespace Domain.StudentDomain.Events
{
    public sealed class StudentHired : StudentEvent
    {
        public StudentHired(Guid aggregateRootId, ulong version) : base(aggregateRootId, version)
        {
        }
    }
}