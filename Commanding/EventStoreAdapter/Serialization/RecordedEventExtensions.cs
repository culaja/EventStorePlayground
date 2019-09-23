using System;
using System.Text;
using Common.Messaging;
using EventStore.ClientAPI;
using Newtonsoft.Json;

namespace EventStoreAdapter.Serialization
{
    public static class RecordedEventExtensions
    {
        public static IDomainEvent ToDomainEvent(this RecordedEvent recordedEvent)
        {
            var serializedEventString = Encoding.UTF8.GetString(recordedEvent.Data);
            var eventMetaData = EventMetaData.EventMetaDataFrom(recordedEvent);
            try
            {
                return (IDomainEvent)JsonConvert
                    .DeserializeObject(
                        serializedEventString,
                        Type.GetType(eventMetaData.FullEventType));
            }
            catch (Exception ex)
            {
                throw new EventDeserializationException(
                    recordedEvent.EventType,
                    serializedEventString,
                    recordedEvent.EventNumber,
                    ex);
            }
        }
    }
}