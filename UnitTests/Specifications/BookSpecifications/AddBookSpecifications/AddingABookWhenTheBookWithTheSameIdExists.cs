using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using Domain.Commands;
using FluentAssertions;
using Xunit;
using static DomainServices.BookCommandExecutors;
using static UnitTests.TestValues;

namespace UnitTests.Specifications.BookSpecifications.AddBookSpecifications
{
    public sealed class AddingABookWhenTheBookWithTheSameIdExists : SpecificationFor<AddBook>
    {
        protected override IReadOnlyList<IDomainEvent> WhenGiven => Events(
            WarAndPeace1Added);
        
        protected override AddBook AfterExecuting => new AddBook(WarAndPeace1Id, WarAndPeaceName, YearOfPrint2010);

        protected override Func<AddBook, Task<Result>> By() => BookCommandExecutorsWith(Repository);
        
        protected override IReadOnlyList<Action> Outcome => Is(
            () => Result.IsFailure.Should().BeTrue());
    }
}