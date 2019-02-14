using System;

namespace Aggregate.Student.Shared
{
    public sealed class StudentHired : StudentEvent
    {
        public StudentHired(Guid aggregateRootId) : base(aggregateRootId)
        {
        }
    }
}