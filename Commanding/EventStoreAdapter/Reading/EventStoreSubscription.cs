using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Common.Messaging;
using EventStore.ClientAPI;
using Ports;

namespace EventStoreAdapter.Reading
{
    internal sealed class EventStoreSubscription : IEventStoreSubscription
    {
        private readonly EventStoreStreamCatchUpSubscription _catchUpSubscription;
        private readonly BlockingCollection<IDomainEvent> _eventStream;

        public EventStoreSubscription(
            EventStoreStreamCatchUpSubscription catchUpSubscription,
            BlockingCollection<IDomainEvent> eventStream)
        {
            _catchUpSubscription = catchUpSubscription;
            _eventStream = eventStream;
        }
        
        public string Name => _catchUpSubscription.SubscriptionName;

        public IEnumerable<IDomainEvent> Stream => WaitForDomainEventOrBreakIfFinished();

        private IEnumerable<IDomainEvent> WaitForDomainEventOrBreakIfFinished()
        {
            while (TakeDomainEventFromStreamOrBreakIfStreamIsCompleted(out var domainEvent))
            {
                yield return domainEvent;
            }
        }

        private bool TakeDomainEventFromStreamOrBreakIfStreamIsCompleted(out IDomainEvent domainEvent)
        {
            try
            {
                domainEvent = _eventStream.Take();
                return true;
            }
            catch (InvalidOperationException)
            {
                domainEvent = null;
                return false;
            }
        }
        
        public void Stop()
        {
            _catchUpSubscription.Stop();
            _eventStream.CompleteAdding();
        }
        
        public void Dispose()
        {
            Stop();
        }
    }
}