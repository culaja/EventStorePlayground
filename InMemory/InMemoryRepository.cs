using System;
using System.Collections.Generic;
using Common;

namespace InMemory
{
    public abstract class InMemoryRepository<T> : IRepository<T> where T: AggregateRoot
    {
        private readonly Dictionary<Guid, T> _cache = new Dictionary<Guid, T>();
        private readonly UniqueIndexing<T> _uniqueIndexes = new UniqueIndexing<T>();

        protected void AddNewIndex(object key, T value) => _uniqueIndexes.AddIndex(key, value);
        
        protected Maybe<T> ReadIndex(object key) => _uniqueIndexes.GetValueFor(key);

        protected virtual void AddedNew(T aggregateRoot)
        {
        }

        public T AddNew(T aggregateRoot)
        {
            if (_cache.ContainsKey(aggregateRoot.Id))
            {
                throw new AggregateRootWithSameIdAlreadyExists<T>(aggregateRoot.Id);
            }
            
            _cache.Add(aggregateRoot.Id, aggregateRoot);
            AddedNew(aggregateRoot);
            return aggregateRoot;
        }

        public T Borrow(Guid aggregateRootId, Func<T, T> transformer) =>
            transformer(ReadAggregateOrThrowIfDoesntExist(aggregateRootId));

        private T ReadAggregateOrThrowIfDoesntExist(Guid aggregateRootId)
        {
            if (!_cache.TryGetValue(aggregateRootId, out var aggregateRoot))
            {
                throw new AggregateRootDoesntExistInRepositoryException<T>(aggregateRootId);
            }

            return aggregateRoot;
        }
    }
}