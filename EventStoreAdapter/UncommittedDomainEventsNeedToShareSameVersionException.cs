using System;
using System.Collections.Generic;
using Common.Messaging;
using static System.String;

namespace EventStoreAdapter
{
    public sealed class UncommittedDomainEventsNeedToShareSameVersionException : Exception
    {
        public UncommittedDomainEventsNeedToShareSameVersionException(IReadOnlyList<IDomainEvent> domainEvents)
            : base($"All uncommitted domain events need to contain same version. Uncommitted events: {Join(",", domainEvents)}")
        {   
        }
    }
}