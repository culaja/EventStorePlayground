using Common.Messaging;

namespace Ports.Messaging
{
    public interface IRemoteMessageBus
    {
        IDomainEvent Send(IDomainEvent e);
    }
}