﻿using System;
using System.Collections.Generic;
using System.Reflection;
using Common.Messaging;

namespace Common
{
    public abstract class AggregateRoot
    {
        private Maybe<AggregateId> _maybeAggregateId = Maybe<AggregateId>.None;

        public AggregateId Id => _maybeAggregateId
            .Ensure(m => m.HasValue,
                () => throw new InvalidOperationException("Aggregate Id needs to be set during object creation in order to use the aggregate."))
            .Value;
        
        public long Version { get; private set; } = -1;

        public long OriginalVersion => Version - DomainEvents.Count;
        
        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;
        
        protected AggregateRoot()
        {
        }

        protected void SetAggregateId(AggregateId aggregateId) => _maybeAggregateId = aggregateId;

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
        
        protected AggregateRoot ApplyChange(IDomainEvent e)
        {
            ApplyChange(e, true);
            return this;
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