using System.Collections.Generic;

namespace Common.Messaging
{
    public abstract class DomainEvent : ValueObject<DomainEvent>, IDomainEvent
    {
        public string AggregateType { get; }
        
        public string AggregateId { get; }
        
        public DomainEvent(string aggregateType, string aggregateId)
        {
            AggregateType = aggregateType;
            AggregateId = aggregateId;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return AggregateType;
            yield return AggregateId;
        }
    }
}