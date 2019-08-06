using System.Collections.Concurrent;
using Common;
using Common.Messaging;
using EventStore.ClientAPI;
using EventStoreAdapter.Serialization;
using Ports;
using static EventStore.ClientAPI.CatchUpSubscriptionSettings;
using static EventStoreAdapter.EventStoreConnectionProvider;

namespace EventStoreAdapter
{
    public sealed class EventStoreSubscriber : IEventStoreSubscriber
    {
        private readonly string _connectionString;
        private readonly string _eventStoreName;

        public EventStoreSubscriber(string connectionString, string eventStoreName)
        {
            _connectionString = connectionString;
            _eventStoreName = eventStoreName;
        }
        
        public IEventStoreSubscription SubscribeToAggregateTypeEvents<T>(long lastCheckpoint = 0) where T : AggregateRoot => 
            EventStoreSubscriptionFrom($"aggtype-{typeof(T).Name}", lastCheckpoint);

        public IEventStoreSubscription SubscribeToAggregateEvents<T>(AggregateId aggregateId, long lastCheckpoint = 0) where T : AggregateRoot => 
            EventStoreSubscriptionFrom(aggregateId.ToStreamName<T>(_eventStoreName), lastCheckpoint);

        public IEventStoreSubscription SubscribeToEventsOfType<T>(long lastCheckpoint = 0) where T : IDomainEvent => 
            EventStoreSubscriptionFrom($"$et-{typeof(T).Name}", lastCheckpoint);

        private IEventStoreSubscription EventStoreSubscriptionFrom(string streamName, long lastCheckpoint)
        {
            var connection = GrabSingleEventStoreConnectionFor(_connectionString).Result;
            var eventStream = new BlockingCollection<IDomainEvent>();
            
            var catchUpSubscription = connection.SubscribeToStreamFrom(
                streamName, 
                lastCheckpoint == 0 ? (long?)null : lastCheckpoint, 
                Default,
                (_, x) => eventStream.Add(x.Event.ToDomainEvent()));
            
            return new EventStoreSubscription(catchUpSubscription, eventStream);
        }
    }
}