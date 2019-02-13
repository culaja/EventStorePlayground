using Common;
using Common.Messaging;
using Ports.Messaging;

namespace CommonServices
{
    public sealed class AggregateRootCreatedRemoteNotifierHandler : EventHandler<AggregateRootCreated>
    {
        private readonly IRemoteMessageBus _remoteMessageBus;

        public AggregateRootCreatedRemoteNotifierHandler(IRemoteMessageBus remoteMessageBus)
        {
            _remoteMessageBus = remoteMessageBus;
        }

        public override Result Handle(AggregateRootCreated e) => _remoteMessageBus
            .Send(e)
            .ToOkResult();
    }
}