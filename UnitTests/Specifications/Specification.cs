using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using EventStoreRepository;
using Ports;
using static UnitTests.Specifications.PreparedEventStoreAppendEvent;

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

            foreach (var preparedEventStoreAppendEvent in GroupGivenEventsPerAggregate())
            {
                _eventStoreAppender.AppendAsync(
                    preparedEventStoreAppendEvent.AggregateId,
                    preparedEventStoreAppendEvent.EventsToAppend,
                    preparedEventStoreAppendEvent.ExpectedVersion);
            }

            When()(CommandToExecute).Result
                .OnBoth(r =>
                {
                    Result = r;
                    return r;
                });
        }

        private IReadOnlyList<PreparedEventStoreAppendEvent> GroupGivenEventsPerAggregate() =>
            PrepareEventsForEventStoreAppend(_givenDomainEvents);

        protected abstract T CommandToExecute { get; }

        protected abstract IEnumerable<IDomainEvent> Given();

        protected abstract Func<T, Task<Result>> When();
        
        protected Result Result { get; private set; }

        protected IReadOnlyList<IDomainEvent> ProducedEvents => 
            _eventStoreAppender.GetAllEventsStartingFrom(_givenDomainEvents.Count);
    }
}