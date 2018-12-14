using System;
using System.Collections.Generic;

namespace Common
{
    public abstract class DomainEvent<T> : ValueObject<DomainEvent<T>>, IDomainEvent
        where T : AggregateRoot
    {
        public Guid AggregateRootId { get; }

        public Type AggregateRootType => typeof(T);

        protected DomainEvent(Guid aggregateRootId)
        {
            AggregateRootId = aggregateRootId;
        }

        public T ApplyTo(T aggregateRoot)
        {
            return (T)ApplyTo((AggregateRoot)aggregateRoot);
        }

        public AggregateRoot ApplyTo(AggregateRoot aggregateRoot)
        {
            var applyMethodInfo = AggregateRootType.GetMethod("Apply", new[] { GetType() });

            if (applyMethodInfo == null)
            {
                throw new InvalidOperationException($"Aggregate '{typeof(T).Name}' can't apply '{GetType().Name}' event type.");
            }
            
            applyMethodInfo.Invoke(aggregateRoot, new object[] {this});

            return aggregateRoot;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return AggregateRootId;
            yield return AggregateRootType;
        }
    }
}