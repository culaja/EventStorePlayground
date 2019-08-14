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
            _givenDomainEvents = WhenGiven().ToList();

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
            Result = Through()(AfterExecutingCommand).Result;

        private IReadOnlyList<GivenAggregateEvents> GroupGivenEventsPerAggregate() => 
            PrepareGivenAggregateEvents(_givenDomainEvents);

        protected abstract IEnumerable<IDomainEvent> WhenGiven();
        
        protected abstract T AfterExecutingCommand { get; }

        protected abstract Func<T, Task<Result>> Through();
        
        protected Result Result { get; private set; }

        protected IReadOnlyList<IDomainEvent> ProducedEvents => 
            _eventStoreAppender.GetAllEventsStartingFrom(_givenDomainEvents.Count);
    }
}