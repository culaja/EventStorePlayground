using System.Collections.Generic;
using Common;

namespace EventStoreAdapter.Reading
{
    public sealed class AggregateTypeProjection<T> : ValueObject<AggregateTypeProjection<T>> where T : AggregateRoot
    {
        private readonly string _eventStoreName;

        private AggregateTypeProjection(string eventStoreName)
        {
            _eventStoreName = eventStoreName;
        }
        
        public static AggregateTypeProjection<T> AggregateTypeProjectionFor(string eventStoreName)
            => new AggregateTypeProjection<T>(eventStoreName);
        
        public string ProjectionName => $"{_eventStoreName}_{typeof(T).Name}_Projection";

        public string StreamName => $"{_eventStoreName}_{typeof(T).Name}";
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ProjectionName;
            yield return StreamName;
        }
    }
}