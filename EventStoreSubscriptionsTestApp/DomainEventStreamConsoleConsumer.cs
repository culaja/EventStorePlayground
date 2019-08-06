using System;
using System.Collections.Generic;
using System.Threading;
using Common.Messaging;

namespace EventStoreSubscriptionsTestApp
{
    public sealed class DomainEventStreamConsoleConsumer : IDisposable
    {
        private readonly string _consumerName;
        private readonly string _predefinedText;
        private readonly Thread _thread;

        public DomainEventStreamConsoleConsumer(
            string consumerName, 
            IEnumerable<IDomainEvent> domainEvents,
            string predefinedText)
        {
            _consumerName = consumerName;
            _predefinedText = predefinedText;
            _thread = new Thread(() => Worker(domainEvents));
            _thread.Start();
        }

        private void Worker(IEnumerable<IDomainEvent> domainEvents)
        {
            foreach (var domainEvent in domainEvents)
            {
                Console.WriteLine($"{_predefinedText}[{_consumerName}] {domainEvent}");
            }
        }

        public void Dispose()
        {
            _thread.Join();
        }
    }
}