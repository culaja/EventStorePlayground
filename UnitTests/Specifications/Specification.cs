using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using EventStoreRepository;
using Ports;
using static UnitTests.Specifications.GivenAggregateEvents;

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

            ApplyGivenEventsToTheEventStore();
            ExecuteCommandAndStoreResult();
        }

        private void ApplyGivenEventsToTheEventStore()
        {
            foreach (var givenAggregateEvents in GroupGivenEventsPerAggregate())
            {
                _eventStoreAppender.AppendAsync(
                    givenAggregateEvents.AggregateId,
                    givenAggregateEvents.EventsToAppend,
                    -1);
            }
        }
        
        private void ExecuteCommandAndStoreResult() => 
            Result = When()(CommandToExecute).Result;

        private IReadOnlyList<GivenAggregateEvents> GroupGivenEventsPerAggregate() => 
            PrepareGivenAggregateEvents(_givenDomainEvents);

        protected abstract T CommandToExecute { get; }

        protected abstract IEnumerable<IDomainEvent> Given();

        protected abstract Func<T, Task<Result>> When();
        
        protected Result Result { get; private set; }

        protected IReadOnlyList<IDomainEvent> ProducedEvents => 
            _eventStoreAppender.GetAllEventsStartingFrom(_givenDomainEvents.Count);
    }
}