using System;

namespace EventStoreAdapter.Serialization
{
    public sealed class EventMetaDataDeserializationException : Exception
    {
        public EventMetaDataDeserializationException(
            string eventTypeName, 
            string serializedEventMetaData,
            long eventNumber,
            Exception innerException)
            : base($"Failed to deserialize event meta data for event type type '{eventTypeName}' with number {eventNumber} and meta data content: {serializedEventMetaData}.", innerException)
        {
        }
    }
}