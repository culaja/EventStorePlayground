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
        
        public async Task<Result> InsertNew<T>(T newAggregate) where T : AggregateRoot, new()
        {
            try
            {
                await _eventStore.AppendAsync(newAggregate.Id, newAggregate.DomainEvents, -1);
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

            if (aggregateEvents.Count > 0)
            {
                var aggregateRoot = ReconstructAggregateFrom<T>(aggregateEvents);
                return await aggregateTransformer(aggregateRoot)
                    .OnSuccess(a => CommitUncommittedDomainEventsFrom(a));
            }

            return Fail($"Aggregate {typeof(T).Name} with Id '{aggregateId}' doesn't exist.");
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
                    aggregateRoot.OriginalVersion);
                return Ok();
            }
            catch (VersionMismatchException e)
            {
                return Fail(e.Message);
            }
        }
    }
}