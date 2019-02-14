using Aggregate.Student.Shared;
using Common;
using Common.Messaging;
using Ports.Messaging;

namespace DomainServices.StudentHandlers.Events
{
    public sealed class StudentRemoteNotifierHandler : EventHandler<StudentEvent>
    {
        private readonly IRemoteMessageBus _remoteMessageBus;

        public StudentRemoteNotifierHandler(IRemoteMessageBus remoteMessageBus)
        {
            _remoteMessageBus = remoteMessageBus;
        }

        public override Result Handle(StudentEvent e) => _remoteMessageBus
            .Send(e)
            .ToOkResult();
    }
}