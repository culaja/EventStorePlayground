using System;

namespace Common
{
    public sealed class AggregateRootDoesntExistInRepositoryException<T> : Exception where T : AggregateRoot
    {
        public AggregateRootDoesntExistInRepositoryException(Guid id)
            : base($"AggregateRoot with id '{id}' doesn't exist in Aggregate '{typeof(T).Name}'.")
        {
        }
    }
}