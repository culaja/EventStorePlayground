using System;
using Common;

namespace EventStoreAdapter
{
    public sealed class VersionMismatchException : Exception
    {
        public VersionMismatchException(AggregateId id, long expectedVersion, long? nextExpectedVersion)
            : base($"Expected version for aggregate '{id.ToStreamName()}' was {expectedVersion}, but version {nextExpectedVersion} is actually expected.")
        {
        }
    }
}