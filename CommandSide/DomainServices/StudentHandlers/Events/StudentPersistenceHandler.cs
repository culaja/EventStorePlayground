using Aggregate.Student.Shared;
using Common;
using Common.Messaging;
using Ports.EventStore;

namespace DomainServices.StudentHandlers.Events
{
    public sealed class StudentPersistenceHandler : EventHandler<StudentEvent>
    {
        private readonly IEventStore _eventStore;

        public StudentPersistenceHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public override Result Handle(StudentEvent e) => _eventStore
            .Append(e)
            .ToOkResult();
    }
}