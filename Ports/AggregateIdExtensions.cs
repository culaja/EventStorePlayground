using Common;

namespace Ports
{
    public static class AggregateIdExtensions
    {
        public static string ToStreamName<T>(this AggregateId aggregateId, string eventStoreName) where T : AggregateRoot
            => $"{eventStoreName}_{typeof(T).Name}_{aggregateId}";
    }
}