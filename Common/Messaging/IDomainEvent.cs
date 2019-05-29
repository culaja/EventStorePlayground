namespace Common.Messaging
{
    public interface IDomainEvent : IMessage
    {
        long Version { get; }

        IDomainEvent SetVersion(long version);
    }
}