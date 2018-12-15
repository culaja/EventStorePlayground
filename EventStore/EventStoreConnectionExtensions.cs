using Common;
using EventStore.ClientAPI;
using static EventStore.EventStoreProvider;

namespace EventStore
{
    public static class EventStoreConnectionExtensions
    {
        public static StreamEventsSlice ReadFirstStreamEventsSliceFor<T>(
            this IEventStoreConnection connection,
            string eventStoreUniverse) where T : AggregateRoot 
            => 
                connection.ReadStreamEventsForwardAsync(
                    ResolveStreamNameFrom(eventStoreUniverse, typeof(T)),
                    0,
                    256,
                    false).Result;
        
        public static StreamEventsSlice ReadNextStreamEventsSliceFor<T>(
            this IEventStoreConnection connection,
            StreamEventsSlice lastSlice,
            string eventStoreUniverse) where T : AggregateRoot 
            => 
                connection.ReadStreamEventsForwardAsync(
                    ResolveStreamNameFrom(eventStoreUniverse, typeof(T)),
                    lastSlice.NextEventNumber,
                    256,
                    false).Result;
    }
}