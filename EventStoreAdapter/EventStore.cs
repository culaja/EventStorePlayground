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

        public MyEventStore(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public async Task<IReadOnlyList<IDomainEvent>> LoadAllEventsForAsync(AggregateId aggregateId)
        {
            var connection = await GrabSingleEventStoreConnectionFor(_connectionString);
            var resolvedEvents = await connection.ReadAllStreamEventsForward(aggregateId.ToStreamName());
            return resolvedEvents.Select(e => e.Deserialize()).ToList();
        }

        public async Task<Nothing> AppendAsync(AggregateId aggregateId, IReadOnlyList<IDomainEvent> domainEvents)
        {
            if (domainEvents.Count > 0)
            {
                var connection = await GrabSingleEventStoreConnectionFor(_connectionString);

                var expectedVersion = domainEvents.First().Version - 1;
                var results = await connection.ConditionalAppendToStreamAsync(
                    aggregateId.ToStreamName(),
                    expectedVersion,
                    domainEvents.Select(e => e.Serialize()));

                switch (results.Status)
                {
                    case Succeeded:
                        return NotAtAll;
                    case VersionMismatch:
                        throw new VersionMismatchException(aggregateId, expectedVersion, results.NextExpectedVersion);
                    case StreamDeleted:
                        throw new StreamDeletedException(aggregateId);
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            return NotAtAll;
        }
    }
}