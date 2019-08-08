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
        private readonly IEventStoreAppender _eventStore;

        public Repository(IEventStoreAppender eventStore)
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
            catch (VersionMismatchException)
            {
                return Fail($"Aggregate '{typeof(T).Name}' with Id '{newAggregate.Id}' already exists in the repository.");
            }
        }

        public async Task<Result> Borrow<T>(AggregateId aggregateId, Func<T, Result<T>> aggregateTransformer) where T : AggregateRoot, new()
        {
            var aggregateEvents = await _eventStore.AsyncLoadAllEventsFor(aggregateId);

            if (aggregateEvents.Count > 0)
            {
                return await aggregateTransformer(ReconstructAggregateFrom<T>(aggregateEvents))
                    .OnSuccess(aggregateRoot => CommitUncommittedDomainEventsFromAggregate<T>(aggregateRoot));
            }

            return Fail($"Aggregate {typeof(T).Name} with Id '{aggregateId}' doesn't exist.");
        }

        private static T ReconstructAggregateFrom<T>(IReadOnlyList<IDomainEvent> domainEvents) where T : AggregateRoot, new()
        {
            var aggregateRoot = new T();
            aggregateRoot.ApplyAll(domainEvents);
            return aggregateRoot;
        }

        private async Task<Result> CommitUncommittedDomainEventsFromAggregate<T>(AggregateRoot aggregateRoot) where T : AggregateRoot, new()
        {
            try
            {
                await _eventStore.AppendAsync(
                    aggregateRoot.Id,
                    aggregateRoot.DomainEvents,
                    aggregateRoot.OriginalVersion);
                return Ok();
            }
            catch (VersionMismatchException)
            {
                return Fail($"Race condition occurred during commiting transaction for aggregate '{typeof(T).Name}' with Id '{aggregateRoot.Id}'. Execute the command again.");
            }
        }
    }
}