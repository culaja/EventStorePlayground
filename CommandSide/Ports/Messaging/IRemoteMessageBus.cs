using System;
using Common;
using Common.Messaging;
using Shared.Common;

namespace Ports.Messaging
{
    public interface IRemoteMessageBus
    {
        IDomainEvent Send(IDomainEvent e);
    }
}