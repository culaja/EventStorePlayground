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
        private readonly IReadOnlyList<IDomainEvent> _givenDomainEvents;

        protected IRepository Repository { get; }
        
        protected SpecificationFor()
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
            Result = By()(AfterExecuting).Result;
        
        private IReadOnlyList<GivenAggregateEvents> GroupGivenEventsPerAggregate() => 
            PrepareGivenAggregateEvents(_givenDomainEvents);

        protected abstract IEnumerable<IDomainEvent> WhenGiven();
        
        protected abstract T AfterExecuting { get; }

        protected abstract Func<T, Task<Result>> By();
        
        protected Result Result { get; private set; }

        protected IReadOnlyList<IDomainEvent> ProducedEvents => 
            _eventStoreAppender.GetAllEventsStartingFrom(_givenDomainEvents.Count);
        
        [Fact]
        public void Checks()
        {
            foreach (var assert in Outcome)
            {
                assert();
            }
        }

        protected virtual IReadOnlyList<Action> Outcome { get; } = new List<Action>();

        protected IReadOnlyList<Action> Is(params Action[] asserts) => asserts.ToList();
    }
}