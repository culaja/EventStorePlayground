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

        public MyEventStore(
            string connectionString,
            string eventStoreName)
        {
            _connectionString = connectionString;
            _eventStoreName = eventStoreName;
        }
        
        public async Task<IReadOnlyList<IDomainEvent>> LoadAllEventsForAsync(AggregateId aggregateId)
        {
            var connection = await GrabSingleEventStoreConnectionFor(_connectionString);
            var resolvedEvents = await connection.ReadAllStreamEventsForward(aggregateId.ToStreamName(_eventStoreName));
            return resolvedEvents.Select(e => e.Deserialize()).ToList();
        }

        public async Task<Nothing> AppendAsync(
            AggregateId aggregateId,
            IReadOnlyList<IDomainEvent> domainEvents,
            long expectedVersion)
        {
            if (domainEvents.Count > 0)
            {
                var connection = await GrabSingleEventStoreConnectionFor(_connectionString);
                var results = await connection.ConditionalAppendToStreamAsync(
                    aggregateId.ToStreamName(_eventStoreName),
                    expectedVersion,
                    domainEvents.Select(e => e.Serialize()));

                switch (results.Status)
                {
                    case Succeeded:
                        return NotAtAll;
                    case VersionMismatch:
                        throw new VersionMismatchException(aggregateId, _eventStoreName, expectedVersion);
                    case StreamDeleted:
                        throw new StreamDeletedException(aggregateId, _eventStoreName);
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            return NotAtAll;
        }
    }
}