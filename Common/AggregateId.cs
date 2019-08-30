using System.Collections.Generic;
using System.Text.RegularExpressions;
using Common.Messaging;

namespace Common
{
    public class AggregateId : Id
    {   
        public string AggregateType { get; }
        
        private readonly string _aggregateId;

        protected AggregateId(string aggregateType, string aggregateId)
        {
            if (!Regex.IsMatch(aggregateType, @"^\S*$")) throw new InvalidAggregateIdException($"Aggregate type '{aggregateType}' can't contain any whitespace character.");
            if (aggregateType.Contains("_")) throw new InvalidAggregateIdException($"Aggregate type '{aggregateType}' can't contain _ characters.");

            if (!Regex.IsMatch(aggregateId, @"^\S*$")) throw new InvalidAggregateIdException($"Aggregate '{aggregateId}' can't contain any whitespace character.");
            if (aggregateId.Contains("_")) throw new InvalidAggregateIdException($"Aggregate '{aggregateId}' can't contain _ characters.");
            
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