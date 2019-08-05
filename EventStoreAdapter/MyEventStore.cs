using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using EventStoreAdapter.Serialization;
using Ports;
using static Common.Nothing;
using static EventStore.ClientAPI.ConditionalWriteStatus;
using static EventStoreAdapter.EventStoreConnectionProvider;

namespace EventStoreAdapter
{
    public sealed class MyEventStore : IEventStore
    {
        private readonly string _connectionString;
        private readonly string _eventStoreName;
        private readonly ILocalMessageBus _localMessageBus;

        public MyEventStore(
            string connectionString,
            string eventStoreName,
            ILocalMessageBus localMessageBus)
        {
            _connectionString = connectionString;
            _eventStoreName = eventStoreName;
            _localMessageBus = localMessageBus;
        }
        
        public async Task<IReadOnlyList<IDomainEvent>> LoadAllEventsForAsync<T>(AggregateId aggregateId) where T : AggregateRoot, new()
        {
            var connection = await GrabSingleEventStoreConnectionFor(_connectionString);
            var resolvedEvents = await connection.ReadAllStreamEventsForward(aggregateId.ToStreamName<T>(_eventStoreName));
            return resolvedEvents.Select(e => e.Event.ToDomainEvent()).ToList();
        }

        public async Task<Nothing> AppendAsync<T>(
            AggregateId aggregateId,
            IReadOnlyList<IDomainEvent> domainEvents,
            long expectedVersion) where T : AggregateRoot, new()
        {
            if (domainEvents.Count > 0)
            {
                var connection = await GrabSingleEventStoreConnectionFor(_connectionString);
                var results = await connection.ConditionalAppendToStreamAsync(
                    aggregateId.ToStreamName<T>(_eventStoreName),
                    expectedVersion,
                    domainEvents.Select(e => e.ToEventData()));

                switch (results.Status)
                {
                    case Succeeded:
                        break;
                    case VersionMismatch:
                        throw new VersionMismatchException(aggregateId.ToStreamName<T>(_eventStoreName), expectedVersion);
                    case StreamDeleted:
                        throw new StreamDeletedException(aggregateId.ToStreamName<T>(_eventStoreName));
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                _localMessageBus.DispatchAll(domainEvents);
            }
            
            return NotAtAll;
        }
    }
}