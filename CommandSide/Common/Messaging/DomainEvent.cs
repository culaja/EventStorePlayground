using System;
using System.Collections.Generic;

namespace Common.Messaging
{
    public abstract class DomainEvent<T> : ValueObject<DomainEvent<T>>, IDomainEvent
        where T : AggregateRoot
    {
        public Guid AggregateRootId { get; }
        public ulong Version { get; private set; }

        public Type AggregateRootType => typeof(T);

        protected DomainEvent(Guid aggregateRootId)
        {
            AggregateRootId = aggregateRootId;
        }
        
        public IDomainEvent SetVersion(ulong version)
        {
            (Version == 0).OnBoth(
                () => Version = version,
                () => throw new InvalidOperationException($"Version already set to {Version} and you want it to set it to {version}"));

            return this;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return AggregateRootId;
            yield return AggregateRootType;
        }
    }
}