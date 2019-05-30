using System;
using Common;

namespace Ports
{
    public sealed class VersionMismatchException : Exception
    {
        public VersionMismatchException(AggregateId id, string eventStoreName, long expectedVersion)
            : base($"Expected version for aggregate '{id.ToStreamName(eventStoreName)}' is {expectedVersion}, but there is more events in the stream.")
        {
        }
    }
}