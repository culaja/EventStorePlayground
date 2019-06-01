namespace Common
{
    public abstract class AggregateId : Id
    {
        public static implicit operator string(AggregateId aggregateId) => aggregateId.ToString();
    }
}