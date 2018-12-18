namespace Common.Commanding
{
    public interface IAggregateRootCommand<T> where T : AggregateRoot
    {
        ISpecification<T> Specification { get; }
    }
}