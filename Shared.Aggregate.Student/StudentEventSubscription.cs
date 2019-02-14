using Common.Messaging;

namespace Aggregate.Student.Shared
{
    public sealed class StudentEventSubscription : IAggregateEventSubscription
    {
        public string AggregateTopicName => "Domain.StudentDomain.Student";
    }
}