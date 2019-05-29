using System.Collections.Generic;
using System.Threading.Tasks;
using EventStore.ClientAPI;

namespace EventStoreAdapter
{
    public static class EventStoreConnectionExtensions
    {
        public static async Task<IReadOnlyList<ResolvedEvent>> ReadAllStreamEventsForward(
            this IEventStoreConnection eventStoreConnection,
            string streamName)
        {
            List<ResolvedEvent> resolvedEvents = new List<ResolvedEvent>();
            StreamEventsSlice streamEventsSlice;
            do
            {
                streamEventsSlice = await eventStoreConnection.ReadStreamEventsForwardAsync(streamName, 0, 10000, false);
                resolvedEvents.AddRange(streamEventsSlice.Events);
            } while (streamEventsSlice.IsEndOfStream);

            return resolvedEvents;
        }
    }
}