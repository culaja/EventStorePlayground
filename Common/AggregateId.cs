using System.Collections.Generic;
using Common.Messaging;

namespace Common
{
    public class AggregateId : Id
    {
        private readonly string _aggregateId;
        public string AggregateType { get; }

        protected AggregateId(string aggregateType, string aggregateId)
        {
            _aggregateId = aggregateId;
            AggregateType = aggregateType;
        }

        public static implicit operator string(AggregateId aggregateId) => aggregateId.ToString();
        
        protected sealed override IEnumerable<object> GetEqualityComponents()
        {
            yield return AggregateType;
        }
        
        public override string ToString() => _aggregateId;

        public static AggregateId ExtractAggregateIdFromDomainEvent(IDomainEvent domainEvent) =>
            new AggregateId(domainEvent.AggregateType, domainEvent.AggregateId);
    }
}