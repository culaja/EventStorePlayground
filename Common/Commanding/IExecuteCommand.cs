namespace Common.Commanding
{
    public interface IExecuteCommand<in T, out TK>
        where T : IAggregateRootCommand<TK>
        where TK : AggregateRoot
    {
        TK Execute(T command);
    }
}