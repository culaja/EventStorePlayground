using Common;

namespace Ports
{
    public static class AggregateIdExtensions
    {
        public static string ToStreamName(this AggregateId aggregateId, string eventStoreName) =>
            $"{eventStoreName}_{aggregateId.AggregateName}_{aggregateId}";
    }
}