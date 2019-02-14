using Common.Messaging;

namespace QueryCommon
{
    public interface IHandle<in T> where T : IDomainEvent
    {
        void Handle(T e);
    }
}