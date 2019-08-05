using System.Collections.Generic;

namespace Common.Messaging
{
    public abstract class DomainEvent : ValueObject<DomainEvent>, IDomainEvent
    {
        public string AggregateType { get; }
        
        public DomainEvent(string aggregateType)
        {
            AggregateType = aggregateType;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return AggregateType;
        }
    }
}