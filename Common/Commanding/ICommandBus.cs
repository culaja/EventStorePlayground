namespace Common.Commanding
{
    public interface ICommandBus
    {
        void Enqueue(IAggregateRootCommand aggregateRootCommand);
    }
}