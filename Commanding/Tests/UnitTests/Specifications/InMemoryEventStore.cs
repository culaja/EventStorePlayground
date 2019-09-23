using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using Ports;
using static Common.Nothing;

namespace UnitTests.Specifications
{
    public sealed class InMemoryEventStore : IEventStoreAppender
    {
        private readonly Dictionary<AggregateId, List<IDomainEvent>> _domainEventsPerAggregate = new Dictionary<AggregateId, List<IDomainEvent>>();
        private readonly List<IDomainEvent> _allDomainEvents = new List<IDomainEvent>();
        
        public Task<IReadOnlyList<IDomainEvent>> AsyncLoadAllEventsFor(AggregateId aggregateId)
        {
            if (!_domainEventsPerAggregate.TryGetValue(aggregateId.ToPureAggregateId(), out var aggregateDomainEvents))
            {
                aggregateDomainEvents = new List<IDomainEvent>();
            }

            return Task.FromResult(aggregateDomainEvents as IReadOnlyList<IDomainEvent>);
        }

        public Task<Nothing> AppendAsync(AggregateId aggregateId, IReadOnlyList<IDomainEvent> domainEvents, long expectedVersion)
        {
            if (!_domainEventsPerAggregate.TryGetValue(aggregateId.ToPureAggregateId(), out var aggregateDomainEvents))
            {
                aggregateDomainEvents = new List<IDomainEvent>();
                _domainEventsPerAggregate.Add(aggregateId.ToPureAggregateId(), aggregateDomainEvents);
            }

            PreventAppendIfExpectedVersionDoesntMatchWithNumberOfPreviouslyAppliedEventsForAggregate(
                aggregateId.ToPureAggregateId(),
                expectedVersion,
                aggregateDomainEvents);

            aggregateDomainEvents.AddRange(domainEvents);
            _allDomainEvents.AddRange(domainEvents);

            return Task.FromResult(NotAtAll);
        }
        
        private void PreventAppendIfExpectedVersionDoesntMatchWithNumberOfPreviouslyAppliedEventsForAggregate(
            AggregateId aggregateId,
            long expectedVersion,
            IReadOnlyList<IDomainEvent> previouslyAppliedEvents)
        {
            if (expectedVersion != previouslyAppliedEvents.Count - 1)
            {
                throw new VersionMismatchException(aggregateId.ToStreamName("Test"), expectedVersion);
            }
        }

        public IReadOnlyList<IDomainEvent> GetAllEventsStartingFrom(int position) =>
            _allDomainEvents.Skip(position).ToList();
    }
}