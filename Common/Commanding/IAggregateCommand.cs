namespace Common.Commanding
{
    public interface ISpecificationBasedAggregateCommand<T> where T : AggregateRoot
    {
        ISpecification<T> Specification { get; }
    }
}