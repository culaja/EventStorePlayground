using System;

namespace Ports
{
    public sealed class StreamDeletedException : Exception
    {
        public StreamDeletedException(string streamName)
            : base($"Stream deleted for aggregate '{streamName}'")
        {
        }
    }
}