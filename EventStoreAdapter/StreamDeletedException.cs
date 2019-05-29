using System;
using Common;

namespace EventStoreAdapter
{
    public sealed class StreamDeletedException : Exception
    {
        public StreamDeletedException(AggregateId id)
            : base($"Stream deleted for aggregate '{id.ToStreamName()}'")
        {
        }
    }
}