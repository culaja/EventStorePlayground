namespace Common.Commanding
{
    public interface ICommandBus
    {
        void Enqueue(INewAggregateCommand newAggregateCommand);
        void Enqueue<T>(ISpecificationBasedAggregateCommand<T> specificationBasedAggregateCommand) where T : AggregateRoot;
    }
}