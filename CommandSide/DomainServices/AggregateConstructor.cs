using System;
using Aggregate.Student.Shared;
using Domain.StudentDomain;
using Ports.EventStore;
using Ports.Repositories;

namespace DomainServices
{
    public sealed class AggregateConstructor
    {
        private readonly IEventStore _eventStore;
        private readonly IStudentRepository _studentRepository;

        public AggregateConstructor(
            IEventStore eventStore,
            IStudentRepository studentRepository)
        {
            _eventStore = eventStore;
            _studentRepository = studentRepository;
        }

        public void ReconstructAllAggregates()
        {
            Console.WriteLine("Started applying events " + DateTime.Now);
            _eventStore.ApplyAllTo<Student, StudentCreated, StudentEventSubscription>(_studentRepository);
            Console.WriteLine("Finished applying events " + DateTime.Now);
        }
    }
}