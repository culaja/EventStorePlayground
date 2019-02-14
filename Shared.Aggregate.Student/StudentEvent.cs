using System;
using Common.Messaging;

namespace Aggregate.Student.Shared
{
    public abstract class StudentEvent : DomainEvent
    {
        private static readonly string StudentAggregateTopicName = new StudentEventSubscription().AggregateTopicName;
        
        protected StudentEvent(Guid aggregateRootId) : base(aggregateRootId, StudentAggregateTopicName)
        {
        }
    }
}