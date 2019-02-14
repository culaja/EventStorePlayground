using System.Collections.Generic;
using Common.Messaging;

namespace Ports
{
    public interface IEventStoreReader
    {
        IEnumerable<IDomainEvent> LoadAll();
    }
}