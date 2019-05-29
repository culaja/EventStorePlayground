using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using EventStore.ClientAPI;
using EventStoreAdapter.Serialization;
using Ports;
using static Common.Nothing;
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
                await connection.ConditionalAppendToStreamAsync(
                    aggregateId.ToStreamName(),
                    domainEvents.First().Version,
                    domainEvents.Select(e => e.Serialize()));
            }
            
            return NotAtAll;
        }
    }
}