using System;
using System.Text;
using Common.Messaging;
using EventStore.ClientAPI;
using Newtonsoft.Json;
using static EventStoreAdapter.Serialization.EventMetaData;

namespace EventStoreAdapter.Serialization
{

    public static class IDomainEventExtensions
    {
        public static EventData ToEventData(this IDomainEvent e) =>
            new EventData(
                Guid.NewGuid(), 
                e.GetType().Name,
                true,
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(e)),
                EventMetaDataFrom(e).ToByteArray());
    }
}