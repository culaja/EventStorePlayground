using System;

namespace Shared.Common
{
    public abstract class SharedEvent 
    {
        public Guid AggregateRootId { get; set; }
        public ulong Version { get; set; }
    }
}