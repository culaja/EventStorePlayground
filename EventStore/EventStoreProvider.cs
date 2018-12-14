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
        public void Append(IDomainEvent domainEvent)
        {
            var newEvent = new EventData(
                NewGuid(),
                nameof(IDomainEvent),
                false,
                Serialize(domainEvent),
                null);

            KeptConnection.AppendToStreamAsync(
                domainEvent.AggregateRootType.FullName,
                ExpectedVersion.Any,
                newEvent).Wait();
        }

        public IEnumerable<IDomainEvent> LoadAllForAggregateStartingFrom<T>(int position = 0) where T : AggregateRoot
        {
            for (var slice = KeptConnection.ReadFirstStreamEventsSliceFor<T>();
                ;
                slice = KeptConnection.ReadNextStreamEventsSliceFor<T>(slice))
            {
                foreach (var resolvedEvent in slice.Events)
                    yield return DeserializeToDomainEvent(
                        resolvedEvent.Event.Data);

                if (slice.IsEndOfStream) break;
            }
        }
    }
}