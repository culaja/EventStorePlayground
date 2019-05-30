using System;
using Common;

namespace Ports
{
    public sealed class StreamDeletedException : Exception
    {
        public StreamDeletedException(AggregateId id, string eventStoreName)
            : base($"Stream deleted for aggregate '{id.ToStreamName(eventStoreName)}'")
        {
        }
    }
}