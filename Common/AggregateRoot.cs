using System;
using System.Collections.Generic;
using System.Reflection;
using Common.Messaging;

namespace Common
{
    public abstract class AggregateRoot
    {
        public AggregateId Id { get; }
        public long Version { get; private set; } = -1;
        
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
        
        public AggregateRoot ApplyAll(IReadOnlyList<IDomainEvent> domainEvents)
        {
            foreach (var e in domainEvents)
            {
                ApplyChange(e, false);
            }
            
            return this;
        }
        
        protected void ApplyChange(IDomainEvent e)
        {
            ApplyChange(e, true);
        }
        
        private void ApplyChange(IDomainEvent e, bool isNew)
        {
            var applyMethodInfo = GetType().GetMethod("Apply", BindingFlags.NonPublic | BindingFlags.Instance, null,  new[] { e.GetType() }, null);

            if (applyMethodInfo == null)
            {
                throw new InvalidOperationException($"Aggregate '{GetType().Name}' can't apply '{e.GetType().Name}' event type.");
            }
            
            applyMethodInfo.Invoke(this, new object[] {e});

            IncrementedVersion();
            if (isNew)
            {
                _domainEvents.Add(e);
            }
        }
        
        private void IncrementedVersion() => ++Version;
    }
}