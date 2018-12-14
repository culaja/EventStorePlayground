using System;
using System.Collections.Generic;
using Common;
using Ports;
using static Common.AggregateRoot;

namespace InMemory
{
    public sealed class InMemoryRepository<T> : IRepository<T> where T: AggregateRoot
    {
        private readonly Dictionary<Guid, T> _cache = new Dictionary<Guid, T>();

        public T Borrow(Guid aggregateRootId, Func<T, T> transformer) =>
            transformer(ReadAggregateOrAddNewToDictionary(aggregateRootId));

        private T ReadAggregateOrAddNewToDictionary(Guid aggregateRootId)
        {
            if (!_cache.TryGetValue(aggregateRootId, out var aggregateRoot))
            {
                _cache.Add(aggregateRootId, CreateNewWith<T>(aggregateRootId));
            }

            return aggregateRoot;
        }
    }
}