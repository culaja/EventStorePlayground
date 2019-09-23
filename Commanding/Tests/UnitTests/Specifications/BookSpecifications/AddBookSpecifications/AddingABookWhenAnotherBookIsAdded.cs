using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using Domain.Commands;
using FluentAssertions;
using static DomainServices.BookCommandExecutors;
using static UnitTests.TestValues;

namespace UnitTests.Specifications.BookSpecifications.AddBookSpecifications
{
    public sealed class AddingABookWhenAnotherBookIsAdded : SpecificationFor<AddBook>
    {
        protected override IReadOnlyList<IDomainEvent> WhenGiven => Events(
            WarAndPeace1IsAdded);
        
        protected override AddBook AfterExecuting => new AddBook(WarAndPeace2Id, WarAndPeaceName, YearOfPrint2010);

        protected override Func<AddBook, Task<Result>> By() => BookCommandExecutorsWith(Repository);
        
        protected override IReadOnlyList<Action> Outcome => Is(
            () => Result.IsSuccess.Should().BeTrue(),
            () => ProducedEvents.Should().Contain(WarAndPeace2IsAdded));
    }
}