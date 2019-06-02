using System.Collections.Generic;

namespace Common.Messaging
{
    public interface ILocalMessageBus
    {
        Nothing DispatchAll(IReadOnlyList<IMessage> messages);

        Nothing Dispatch(IMessage message);
    }
}