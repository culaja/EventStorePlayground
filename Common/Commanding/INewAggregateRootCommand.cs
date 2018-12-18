namespace Common.Commanding
{
    public interface INewAggregateRootCommand<out T> where T : AggregateRoot
    {
        T CreateNew();
    }
}