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
            if (!_domainEventsPerAggregate.TryGetValue(aggregateId, out var aggregateDomainEvents))
            {
                aggregateDomainEvents = new List<IDomainEvent>();
            }

            return Task.FromResult(aggregateDomainEvents as IReadOnlyList<IDomainEvent>);
        }

        public Task<Nothing> AppendAsync(AggregateId aggregateId, IReadOnlyList<IDomainEvent> domainEvents, long expectedVersion)
        {
            if (!_domainEventsPerAggregate.TryGetValue(aggregateId, out var aggregateDomainEvents))
            {
                aggregateDomainEvents = new List<IDomainEvent>();
                _domainEventsPerAggregate.Add(aggregateId, aggregateDomainEvents);
            }

            aggregateDomainEvents.AddRange(domainEvents);
            _allDomainEvents.AddRange(domainEvents);

            return Task.FromResult(NotAtAll);
        }

        public IReadOnlyList<IDomainEvent> GetAllEventsStartingFrom(int position) =>
            _allDomainEvents.GetRange(position, _allDomainEvents.Count);
    }
}