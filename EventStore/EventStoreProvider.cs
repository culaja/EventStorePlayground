using System;
using System.Collections.Generic;
using Common;
using EventStore.ClientAPI;
using Ports.EventStore;
using static System.Guid;
using static EventStore.DomainEventSerializer;
using static EventStore.EventStoreConnectionKeeper;

namespace EventStore
{
    public sealed class EventStoreProvider : IEventStore
    {
        private readonly string _eventStoreUniverse;

        /// <param name="eventStoreUniverse">Used as a prefix for each stream, so you can choose your event stream.</param>
        public EventStoreProvider(string eventStoreUniverse)
        {
            _eventStoreUniverse = eventStoreUniverse;
        }
        
        public void Append(IDomainEvent domainEvent)
        {
            var newEvent = new EventData(
                NewGuid(),
                nameof(IDomainEvent),
                false,
                Serialize(domainEvent),
                null);

            KeptConnection.AppendToStreamAsync(
                ResolveStreamNameFrom(_eventStoreUniverse, domainEvent.AggregateRootType),
                ExpectedVersion.Any,
                newEvent).Wait();
        }

        internal static string ResolveStreamNameFrom(
            string eventStoreUniverse,
            Type aggregateRootType) =>
            $"{eventStoreUniverse}_{aggregateRootType.Name}";

        public IEnumerable<IDomainEvent> LoadAllForAggregateStartingFrom<T>(int position = 0) where T : AggregateRoot
        {
            for (var slice = KeptConnection.ReadFirstStreamEventsSliceFor<T>(_eventStoreUniverse);
                ;
                slice = KeptConnection.ReadNextStreamEventsSliceFor<T>(slice, _eventStoreUniverse))
            {
                foreach (var resolvedEvent in slice.Events)
                    yield return DeserializeToDomainEvent(
                        resolvedEvent.Event.Data);

                if (slice.IsEndOfStream) break;
            }
        }
    }
}