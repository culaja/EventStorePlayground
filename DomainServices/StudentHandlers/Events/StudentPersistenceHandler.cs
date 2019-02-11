using Common;
using Common.Messaging;
using Domain.StudentDomain;
using Ports.EventStore;

namespace DomainServices.StudentHandlers.Events
{
    public sealed class StudentPersistenceHandler : EventHandler<DomainEvent<Student>>
    {
        private readonly IEventStore _eventStore;

        public StudentPersistenceHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public override Result Handle(DomainEvent<Student> e) => _eventStore
            .Append(e)
            .ToOkResult();
    }
}