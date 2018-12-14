using System.Collections.Generic;
using Common;

namespace Ports
{
    public interface IEventStore
    {
        void Append(IDomainEvent domainEvent);

        IEnumerable<IDomainEvent> LoadAllStartingFrom(int position = 0);
    }
}