namespace Common
{
    public abstract class AggregateId : Id
    {
        public abstract string AggregateName { get; }
        
        public static implicit operator string(AggregateId aggregateId) => aggregateId.ToString();
    }
}