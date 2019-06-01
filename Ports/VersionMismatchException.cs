using System;
using Common;

namespace Ports
{
    public sealed class VersionMismatchException : Exception
    {
        public VersionMismatchException(string streamName, long expectedVersion)
            : base($"Expected version for aggregate '{streamName}' is {expectedVersion}, but there is more events in the stream.")
        {
        }
    }
}