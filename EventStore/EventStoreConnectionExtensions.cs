using Common;
using EventStore.ClientAPI;

namespace EventStore
{
    public static class EventStoreConnectionExtensions
    {
        public static StreamEventsSlice ReadFirstStreamEventsSliceFor<T>(
            this IEventStoreConnection connection) where T : AggregateRoot 
            => 
                connection.ReadStreamEventsForwardAsync(
                    typeof(T).FullName,
                    0,
                    256,
                    false).Result;
        
        public static StreamEventsSlice ReadNextStreamEventsSliceFor<T>(
            this IEventStoreConnection connection,
            StreamEventsSlice lastSlice) where T : AggregateRoot 
            => 
                connection.ReadStreamEventsForwardAsync(
                    typeof(T).FullName,
                    lastSlice.NextEventNumber,
                    256,
                    false).Result;
    }
}