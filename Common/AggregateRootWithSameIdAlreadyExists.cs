using System;

namespace Common
{
    public sealed class AggregateRootWithSameIdAlreadyExists<T> : Exception where T : AggregateRoot
    {
        public AggregateRootWithSameIdAlreadyExists(Guid id)
            : base($"AggregateRoot with id '{id}' already exists in Aggregate '{typeof(T).Name}'.")
        {
        }
    }
}