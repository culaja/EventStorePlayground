using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using Ports;
using static Common.Result;

namespace EventStoreRepository
{
    public sealed class Repository : IRepository
    {
        private readonly IEventStore _eventStore;

        public Repository(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }
        
        public async Task<Result> Create<T>(Func<T> aggregateCreator) where T : AggregateRoot, new()
        {
            try
            {
                var aggregateRoot = aggregateCreator();
                await _eventStore.AppendAsync(aggregateRoot.Id, aggregateRoot.DomainEvents, -1);
                return Ok();
            }
            catch (VersionMismatchException e)
            {
                return Fail(e.Message);
            }
        }

        public async Task<Result> BorrowBy<T>(AggregateId aggregateId, Func<T, Result<T>> aggregateTransformer) where T : AggregateRoot, new()
        {
            var aggregateEvents = await _eventStore.LoadAllEventsForAsync(aggregateId);
            var aggregateRoot = ReconstructAggregateFrom<T>(aggregateEvents);
            return await aggregateTransformer(aggregateRoot)
                .OnSuccess(a => CommitUncommittedDomainEventsFrom(a));
        }

        private static T ReconstructAggregateFrom<T>(IReadOnlyList<IDomainEvent> domainEvents) where T : AggregateRoot, new()
        {
            var aggregateRoot = new T();
            aggregateRoot.ApplyAll(domainEvents);
            return aggregateRoot;
        }

        private async Task<Result> CommitUncommittedDomainEventsFrom(AggregateRoot aggregateRoot)
        {
            try
            {
                await _eventStore.AppendAsync(
                    aggregateRoot.Id,
                    aggregateRoot.DomainEvents,
                    aggregateRoot.Version);
                return Ok();
            }
            catch (VersionMismatchException e)
            {
                return Fail(e.Message);
            }
        }
    }
}