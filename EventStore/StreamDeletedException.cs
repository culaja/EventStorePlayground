using Ports.EventStore;

namespace EventStore
{
    public sealed class StreamDeletedException : EventStoreException
    {
        public StreamDeletedException(string streamName)
            : base($"Event store stream '{streamName}' has been deleted")
        {
        }
    }
}