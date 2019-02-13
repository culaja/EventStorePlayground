namespace Shared.Common
{
    public interface IAggregateEventSubscription
    {
        string AggregateTopicName { get; }
    }
}