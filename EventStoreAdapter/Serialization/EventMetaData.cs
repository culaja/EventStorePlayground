using System;
using System.Text;
using Common.Messaging;
using EventStore.ClientAPI;
using Newtonsoft.Json;

namespace EventStoreAdapter.Serialization
{
    internal sealed class EventMetaData
    {
        public string AggregateType { get; }
        public string FullEventType { get; }

        public EventMetaData(
            string aggregateType,
            string fullEventType)
        {
            AggregateType = aggregateType;
            FullEventType = fullEventType;
        }
        
        public static EventMetaData EventMetaDataFrom(IDomainEvent domainEvent) =>
            new EventMetaData(
                domainEvent.AggregateType,
                domainEvent.GetType().AssemblyQualifiedName);

        public static EventMetaData EventMetaDataFrom(RecordedEvent recordedEvent)
        {
            var serializedEventMetaDataString = Encoding.UTF8.GetString(recordedEvent.Metadata);
            try
            {
                return JsonConvert.DeserializeObject<EventMetaData>(serializedEventMetaDataString);
            }
            catch (Exception ex)
            {
                throw new EventMetaDataDeserializationException(
                    recordedEvent.EventType, 
                    serializedEventMetaDataString,
                    recordedEvent.EventNumber, 
                    ex);
            }
            
        }

        public byte[] ToByteArray() => Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(this));
    }
}