using Common;
using Common.Messaging;

namespace Ports
{
    public interface IEventStoreSubscriber
    {
        IEventStoreSubscription SubscribeToAggregateTypeEvents<T>(long lastCheckpoint = 0) where T : AggregateRoot;

        IEventStoreSubscription SubscribeToAggregateEvents<T>(AggregateId aggregateId, long lastCheckpoint = 0) where T : AggregateRoot;

        IEventStoreSubscription SubscribeToEventsOfType<T>(long lastCheckpoint = 0) where T : IDomainEvent;
    }
}