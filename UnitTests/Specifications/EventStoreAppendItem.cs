using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Messaging;
using static Common.AggregateId;

namespace UnitTests.Specifications
{
    internal sealed class PreparedEventStoreAppendEvent : ValueObject<PreparedEventStoreAppendEvent>
    {
        public AggregateId AggregateId { get; }
        public IReadOnlyList<IDomainEvent> EventsToAppend { get; }
        public long ExpectedVersion { get; }

        public PreparedEventStoreAppendEvent(
            AggregateId aggregateId,
            IReadOnlyList<IDomainEvent> eventsToAppend,
            long expectedVersion)
        {
            AggregateId = aggregateId;
            EventsToAppend = eventsToAppend;
            ExpectedVersion = expectedVersion;
        }
        
        public static IReadOnlyList<PreparedEventStoreAppendEvent> PrepareEventsForEventStoreAppend(IReadOnlyList<IDomainEvent> domainEvents) =>
            domainEvents.GroupBy(
                    ExtractAggregateIdFromDomainEvent,
                    e => e,
                    (aggregateId, events) =>
                        Enumerable.Range(-1, events.Count)
                            .Map(expectedVersion => new PreparedEventStoreAppendEvent(aggregateId, events, expectedVersion)))
                .Flatten();
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}