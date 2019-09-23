using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using EventStoreRepository;
using Ports;
using Xunit;
using static UnitTests.Specifications.GivenAggregateEvents;

namespace UnitTests.Specifications
{
    public abstract class SpecificationFor<T> where T : ICommand
    {
        private readonly InMemoryEventStore _eventStoreAppender = new InMemoryEventStore();

        protected IRepository Repository { get; }
        
        protected SpecificationFor()
        {
            Repository = new Repository(_eventStoreAppender);

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
            Result = By()(AfterExecuting).Result;
        
        private IReadOnlyList<GivenAggregateEvents> GroupGivenEventsPerAggregate() => 
            PrepareGivenAggregateEvents(WhenGiven);
        
        protected abstract IReadOnlyList<IDomainEvent> WhenGiven { get; }

        protected IReadOnlyList<IDomainEvent> Events(params IDomainEvent[] domainEvents) => domainEvents;
        protected IReadOnlyList<IDomainEvent> NoEvents => new List<IDomainEvent>();
        
        protected abstract T AfterExecuting { get; }

        protected abstract Func<T, Task<Result>> By();
        
        protected Result Result { get; private set; }

        protected IReadOnlyList<IDomainEvent> ProducedEvents => 
            _eventStoreAppender.GetAllEventsStartingFrom(WhenGiven.Count);
        
        [Fact]
        public void Checks()
        {
            foreach (var assert in Outcome)
            {
                assert();
            }
        }

        protected abstract IReadOnlyList<Action> Outcome { get; }

        protected IReadOnlyList<Action> Is(params Action[] asserts) => asserts.ToList();
    }
}