using System;
using System.Collections.Generic;
using System.Reflection;
using Common.Messaging;

namespace Common
{
    public abstract class AggregateRoot
    {
        public AggregateId Id { get; }
        public long Version { get; private set; }
        
        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;
        
        protected AggregateRoot(AggregateId id)
        {
            Id = id;
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
        
        public AggregateRoot ApplyFrom(IDomainEvent e)
        {
            ApplyChange(e, false);
            return this;
        }
        
        protected void ApplyChange(IDomainEvent e)
        {
            ApplyChange(e, true);
        }
        
        private void ApplyChange(IDomainEvent e, bool isNew)
        {
            var expectedVersion = Version + 1;
            if (!isNew && expectedVersion != e.Version)
            {
                throw new InvalidOperationException($"Inconsistent state since aggregate root '{GetType().Name}' with ID '{Id} 'expected to apply event '{e.GetType().Name}' version {expectedVersion} but version {e.Version} is applied instead.");
            }
        
            var applyMethodInfo = GetType().GetMethod("Apply", BindingFlags.NonPublic | BindingFlags.Instance, null,  new[] { e.GetType() }, null);

            if (applyMethodInfo == null)
            {
                throw new InvalidOperationException($"Aggregate '{GetType().Name}' can't apply '{e.GetType().Name}' event type.");
            }
            
            applyMethodInfo.Invoke(this, new object[] {e});

            IncrementedVersion();
            if (isNew)
            {
                e.SetVersion(Version);
                _domainEvents.Add(e);
            }
        }
        
        private void IncrementedVersion() => ++Version;
    }
}