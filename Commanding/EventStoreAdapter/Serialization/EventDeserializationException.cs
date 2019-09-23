using System;

namespace EventStoreAdapter.Serialization
{
    public sealed class EventDeserializationException : Exception
    {
        public EventDeserializationException(
            string eventTypeName, 
            string serializedEvent,
            long eventNumber,
            Exception innerException)
            : base($"Failed to deserialize event of type '{eventTypeName}' with number {eventNumber} and content: {serializedEvent}.", innerException)
        {
        }
    }
}