using System;
using Common.Messaging;
using Domain.StudentDomain;
using Domain.StudentDomain.Events;
using Ports.EventStore;
using Ports.Messaging;
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
            _eventStore.ApplyAllTo(_studentRepository);
            Console.WriteLine("Finished applying events " + DateTime.Now);
        }
    }

    public sealed class RemoteMessageSubscriber
    {
        private readonly IRemoteMessageBus _remoteMessageBus;
        private readonly ILocalMessageBus _localMessageBus;

        public RemoteMessageSubscriber(
            IRemoteMessageBus remoteMessageBus,
            ILocalMessageBus localMessageBus)
        {
            _remoteMessageBus = remoteMessageBus;
            _localMessageBus = localMessageBus;
        }

        public void SubscribeToRemoteMessages()
        {
        }
    }
}