using System;

namespace Common
{
    public sealed class AggregateRootDoesntExistForKeyInRepositoryException<T> : Exception where T : AggregateRoot
    {
        public AggregateRootDoesntExistForKeyInRepositoryException(object key, object value)
            : base($"AggregateRoot '{typeof(T).Name}' doesn't exist for '{key}= {value}'")
        {
        }
    }
}