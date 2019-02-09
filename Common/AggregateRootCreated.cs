using System;
using System.Collections.Generic;
using Common.Messaging;

namespace Common
{
    public sealed class AggregateRootCreated : ValueObject<AggregateRootCreated>, IDomainEvent
    {
        public AggregateRootCreated(Type aggregateRootType, params object[] aggregateRootCreationParameters)
        {
            AggregateRootId = (Guid)aggregateRootCreationParameters[0];
            AggregateRootType = aggregateRootType;
            AggregateRootCreationParameters = aggregateRootCreationParameters;
        }

        public Guid AggregateRootId { get; }
        public Type AggregateRootType { get; }
        public object[] AggregateRootCreationParameters { get; }

        T IDomainEvent.ApplyTo<T>(T aggregateRoot) => aggregateRoot;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return AggregateRootId;
            yield return AggregateRootType;
            foreach (var item in AggregateRootCreationParameters) yield return item;
        }
    }
}