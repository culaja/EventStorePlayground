using System;
using System.Collections.Generic;
using Common;
using Common.Messaging;
using static Common.Result;

namespace InMemory
{
    public abstract class InMemoryRepository<T> : IRepository<T> where T: AggregateRoot
    {
        private readonly IMessageBus _messageBus;
        
        private readonly Dictionary<Guid, T> _cache = new Dictionary<Guid, T>();
        private readonly UniqueIndexing<T> _uniqueIndexes = new UniqueIndexing<T>();

        public InMemoryRepository(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        protected bool ContainsIndex(object key) => _uniqueIndexes.ContainsIndex(key);

        protected void AddNewIndex(object key, T value) => _uniqueIndexes.AddIndex(key, value);
        
        protected Result<T> MaybeReadIndex(object key) => _uniqueIndexes.GetValueFor(key)
            .Unwrap(
                Ok,
                () => Fail<T>($"Can't find '{typeof(T).Name}' with key '{key}'"));

        protected virtual bool ContainsKey(T aggregateRoot)
        {
            return false;
        }

        protected virtual void AddedNew(T aggregateRoot)
        {
        }

        public Result<T> AddNew(T aggregateRoot)
        {
            if (_cache.ContainsKey(aggregateRoot.Id) || ContainsKey(aggregateRoot))
            {
                return Fail<T>($"AggregateRoot with id '{aggregateRoot.Id}' already exists in Aggregate '{typeof(T).Name}'.");
            }
            
            _cache.Add(aggregateRoot.Id, aggregateRoot);
            AddedNew(aggregateRoot);
            return Ok(aggregateRoot);
        }

        public Result<T> BorrowBy(Guid aggregateRootId, Func<T, T> transformer) => ReadAggregateFromCash(aggregateRootId)
            .OnSuccess(transformer)
            .OnSuccess(t => PurgeAllEvents(t));

        private Result<T> ReadAggregateFromCash(Guid aggregateRootId)
        {
            if (!_cache.TryGetValue(aggregateRootId, out var aggregateRoot))
            {
                return Fail<T>($"AggregateRoot with id '{aggregateRootId}' doesn't exist in Aggregate '{typeof(T).Name}'.");
            }

            return Ok(aggregateRoot);
        }

        private T PurgeAllEvents(T aggregateRoot)
        {
            _messageBus.DispatchAll(aggregateRoot.DomainEvents);
            aggregateRoot.ClearDomainEvents();
            return aggregateRoot;
        }
        
    }
}