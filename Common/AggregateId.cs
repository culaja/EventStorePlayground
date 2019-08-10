using System.Collections.Generic;
using Common.Messaging;

namespace Common
{
    public class AggregateId : Id
    {   
        public string AggregateType { get; }
        
        private readonly string _aggregateId;

        protected AggregateId(string aggregateType, string aggregateId)
        {
            AggregateType = aggregateType;
            _aggregateId = aggregateId;
        }

        public static implicit operator string(AggregateId aggregateId) => aggregateId.ToString();
        
        protected sealed override IEnumerable<object> GetEqualityComponents()
        {
            yield return AggregateType;
            yield return _aggregateId;
        }
        
        public override string ToString() => _aggregateId;
        
        public AggregateId ToPureAggregateId() => new AggregateId(AggregateType, _aggregateId);

        public static AggregateId ExtractAggregateIdFromDomainEvent(IDomainEvent domainEvent) =>
            new AggregateId(domainEvent.AggregateType, domainEvent.AggregateId);
    }
}