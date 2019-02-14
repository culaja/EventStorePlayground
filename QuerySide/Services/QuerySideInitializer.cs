using System;
using Aggregate.Student.Shared;
using Common.Messaging;
using Ports;
using StudentViews;

namespace Services
{
    public sealed class QuerySideInitializer
    {
        private readonly IEventStoreReader _eventStoreReader;
        private readonly IRemoteEventSubscriber _remoteEventSubscriber;
        private readonly ILocalMessageBus _localMessageBus;
        private readonly StudentsPerCityView _studentsPerCityView;

        public QuerySideInitializer(
            IEventStoreReader eventStoreReader,
            IRemoteEventSubscriber remoteEventSubscriber,
            ILocalMessageBus localMessageBus,
            StudentsPerCityView studentsPerCityView)
        {
            _eventStoreReader = eventStoreReader;
            _remoteEventSubscriber = remoteEventSubscriber;
            _localMessageBus = localMessageBus;
            _studentsPerCityView = studentsPerCityView;
        }

        public void Initialize()
        {
            SubscribeToDomainEventsAndPassThemToLocalMessageBus();
            PerformIntegrityLoadFromEventStore();
        }

        private void SubscribeToDomainEventsAndPassThemToLocalMessageBus()
        {
            _remoteEventSubscriber.Register<StudentEventSubscription>(e => _localMessageBus.Dispatch(e));
            Console.WriteLine($"Subscribed to all {nameof(StudentEvent)}s ...");
        }

        private void PerformIntegrityLoadFromEventStore()
        {
            Console.WriteLine("Performing integrity read of domain events ...");
            var domainEvents = _eventStoreReader.LoadAll();
            foreach (var e in domainEvents)
            {
                _studentsPerCityView.Apply(e);
            }
            Console.WriteLine("Integrity read finished");
            Console.WriteLine(_studentsPerCityView);
        }
    }
}