using System.Collections.Generic;

namespace Common.Messaging
{
    public interface IMessageBus
    {
        IReadOnlyList<IMessage> DispatchAll(IReadOnlyList<IMessage> messages);

        IMessage Dispatch(IMessage message);
    }
}