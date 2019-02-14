using System;
using System.Collections.Generic;

namespace Common.Messaging
{
    public abstract class DomainEvent : ValueObject<DomainEvent>, IDomainEvent
    {
        public Guid AggregateRootId { get; }
        public string AggregateName { get; }
        public ulong Version { get; private set; }


        protected DomainEvent(Guid aggregateRootId, string aggregateName)
        {
            AggregateRootId = aggregateRootId;
            AggregateName = aggregateName;
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
            yield return AggregateName;
        }
    }
}