namespace Common
{
    public abstract class AggregateId : Id
    {
        public abstract string ToStreamName();
    }
}