using System;

namespace Common.Messaging
{
    public abstract class DomainEvent : ValueObject<DomainEvent>, IDomainEvent
    {
        public long Version { get; set; }
        
        public IDomainEvent SetVersion(long version)
        {
            (Version == 0).OnBoth(
                () => Version = version,
                () => throw new InvalidOperationException($"Version already set to {Version} and you want to set it to {version}"));

            return this;
        }
    }
}