namespace Common.Commanding
{
    public interface ICommandBus<T> where T : AggregateRoot
    {
        void Enqueue(INewAggregateRootCommand<T> newAggregateRootCommand);
        void Enqueue(IAggregateRootCommand<T> aggregateRootCommand);
    }
}