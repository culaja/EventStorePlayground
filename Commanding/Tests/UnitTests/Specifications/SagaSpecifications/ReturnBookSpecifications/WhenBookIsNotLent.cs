using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using Domain.Commands;
using FluentAssertions;
using LibraryEvents.BookEvents;
using LibraryEvents.UserEvents;
using static DomainServices.SagaCommandExecutors;
using static UnitTests.AssertionsHelpers;
using static UnitTests.TestValues;

namespace UnitTests.Specifications.SagaSpecifications.ReturnBookSpecifications
{
    public sealed class WhenBookIsNotLent : SpecificationFor<ReturnBook>
    {
        protected override IReadOnlyList<IDomainEvent> WhenGiven => Events(
            WarAndPeace1IsAdded,
            JohnDoeUserIsAdded);
        
        protected override ReturnBook AfterExecuting => new ReturnBook(WarAndPeace1Id, JohnDoeId);

        protected override Func<ReturnBook, Task<Result>> By() => SagaCommandExecutorsWith(Repository);
        
        protected override IReadOnlyList<Action> Outcome => Is(
            () => Result.IsFailure.Should().BeTrue(),
            () => ProducedEvents.Should().NotContain(EventOf<BookReturned>()),
            () => ProducedEvents.Should().NotContain(EventOf<UserFinishedBookBorrow>()));
    }
}