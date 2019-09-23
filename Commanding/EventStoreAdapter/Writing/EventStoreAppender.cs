using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using EventStore.ClientAPI;
using EventStoreAdapter.Serialization;
using Ports;

namespace EventStoreAdapter.Writing
{
    public sealed class EventStoreAppender : IEventStoreAppender
    {
        private readonly string _connectionString;
        private readonly string _eventStoreName;

        public EventStoreAppender(
            string connectionString,
            string eventStoreName)
        {
            _connectionString = connectionString;
            _eventStoreName = eventStoreName;
        }
        
        public async Task<IReadOnlyList<IDomainEvent>> AsyncLoadAllEventsFor(AggregateId aggregateId)
        {
            var connection = await EventStoreConnectionProvider.GrabSingleEventStoreConnectionFor(_connectionString);
            var resolvedEvents = await connection.ReadAllStreamEventsForward(aggregateId.ToStreamName(_eventStoreName));
            return resolvedEvents.Select(e => e.Event.ToDomainEvent()).ToList();
        }

        public async Task<Nothing> AppendAsync(
            AggregateId aggregateId,
            IReadOnlyList<IDomainEvent> domainEvents,
            long expectedVersion)
        {
            if (domainEvents.Count > 0)
            {
                var connection = await EventStoreConnectionProvider.GrabSingleEventStoreConnectionFor(_connectionString);
                var results = await connection.ConditionalAppendToStreamAsync(
                    aggregateId.ToStreamName(_eventStoreName),
                    expectedVersion,
                    domainEvents.Select(e => e.ToEventData()));

                switch (results.Status)
                {
                    case ConditionalWriteStatus.Succeeded:
                        break;
                    case ConditionalWriteStatus.VersionMismatch:
                        throw new VersionMismatchException(aggregateId.ToStreamName(_eventStoreName), expectedVersion);
                    case ConditionalWriteStatus.StreamDeleted:
                        throw new StreamDeletedException(aggregateId.ToStreamName(_eventStoreName));
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            return Nothing.NotAtAll;
        }
    }
}