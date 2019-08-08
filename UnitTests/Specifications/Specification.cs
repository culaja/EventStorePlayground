using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using EventStoreRepository;
using Ports;
using static Common.AggregateId;
using ICommand = System.Windows.Input.ICommand;

namespace UnitTests.Specifications
{
    public abstract class Specification<T> where T : ICommand
    {
        private readonly InMemoryEventStore _eventStoreAppender = new InMemoryEventStore();
        private readonly IReadOnlyList<IDomainEvent> _givenDomainEvents;

        protected IRepository Repository { get; }
        
        protected Specification()
        {
            Repository = new Repository(_eventStoreAppender);
            _givenDomainEvents = Given().ToList();

            _givenDomainEvents.GroupBy(
                ExtractAggregateIdFromDomainEvent,
                e => e,
                (aggregateId, events) => _eventStoreAppender.AppendAsync(aggregateId, events.ToList(), 0).Result)
                .ToList();

            When()(CommandToExecute).Result
                .OnBoth(r =>
                {
                    Result = r;
                    return r;
                });
        }
        
        protected abstract T CommandToExecute { get; }

        protected abstract IEnumerable<IDomainEvent> Given();

        protected abstract Func<T, Task<Result>> When();
        
        protected Result Result { get; private set; }

        protected IReadOnlyList<IDomainEvent> ProducedEvents => 
            _eventStoreAppender.GetAllEventsStartingFrom(_givenDomainEvents.Count);
    }
}