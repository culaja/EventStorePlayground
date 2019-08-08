namespace Common.Messaging
{
    public interface IDomainEvent : IMessage
    {
        string AggregateType { get; }
        
        string AggregateId { get; }
    }
}