using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using Domain.Commands;
using FluentAssertions;
using LibraryEvents.UserEvents;
using static DomainServices.UserCommandExecutors;
using static UnitTests.AssertionsHelpers;
using static UnitTests.TestValues;

namespace UnitTests.Specifications.UserSpecification.ReturnBookSpecifications
{
    public sealed class WhenUserDoesntBorrowTheBook : SpecificationFor<ReturnBook>
    {
        protected override IReadOnlyList<IDomainEvent> WhenGiven => Events(
            JohnDoeUserIsAdded);
        
        protected override ReturnBook AfterExecuting => new ReturnBook(WarAndPeace1Id, JohnDoeId);

        protected override Func<ReturnBook, Task<Result>> By() => UserCommandExecutorsWith(Repository);
        
        protected override IReadOnlyList<Action> Outcome => Is(
            () => Result.IsFailure.Should().BeTrue(),
            () => ProducedEvents.Should().NotContain(EventOf<UserFinishedBookBorrow>()));
    }
}