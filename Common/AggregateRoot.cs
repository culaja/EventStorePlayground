using System;
using System.Collections.Generic;
using Common.Messaging;

namespace Common
{
    public abstract class AggregateRoot
    {
        public Guid Id { get; }
        protected ulong Version { get; private set; }
        
        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;
        
        protected AggregateRoot(Guid id, ulong version)
        {
            Id = id;
            Version = version;
        }

        protected ulong IncrementedVersion() => ++Version;

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
            var applyMethodInfo = GetType().GetMethod("Apply", new[] { e.GetType() });

            if (applyMethodInfo == null)
            {
                throw new InvalidOperationException($"Aggregate '{GetType().Name}' can't apply '{e.GetType().Name}' event type.");
            }
            
            applyMethodInfo.Invoke(this, new object[] {e});
            
            if (isNew)
            {
                _domainEvents.Add(e);
            }
        }
    }
}