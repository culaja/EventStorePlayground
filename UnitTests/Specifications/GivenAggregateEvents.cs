using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Messaging;
using static Common.AggregateId;

namespace UnitTests.Specifications
{
    internal sealed class GivenAggregateEvents : ValueObject<GivenAggregateEvents>
    {
        public AggregateId AggregateId { get; }
        public IReadOnlyList<IDomainEvent> EventsToAppend { get; }

        public GivenAggregateEvents(
            AggregateId aggregateId,
            IReadOnlyList<IDomainEvent> eventsToAppend)
        {
            AggregateId = aggregateId;
            EventsToAppend = eventsToAppend;
        }

        public static IReadOnlyList<GivenAggregateEvents> PrepareGivenAggregateEvents(
            IReadOnlyList<IDomainEvent> domainEvents) =>
            domainEvents.GroupBy(
                    ExtractAggregateIdFromDomainEvent,
                    (aggregateId, events) =>
                        new GivenAggregateEvents(aggregateId, events.ToList()))
                .ToList();
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return AggregateId;
            yield return EventsToAppend;
        }
    }
}