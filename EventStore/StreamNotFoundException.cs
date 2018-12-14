using Ports.EventStore;

namespace EventStore
{
    public sealed class StreamNotFoundException : EventStoreException
    {
        public StreamNotFoundException(string streamName)
            : base($"Stream '{streamName}' can't be found.")
        {
        }
    }
}