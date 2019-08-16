using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using Domain.Commands;
using FluentAssertions;
using LibraryEvents.UserEvents;
using Xunit;
using static DomainServices.UserCommandExecutors;
using static UnitTests.AssertionsHelpers;
using static UnitTests.TestValues;

namespace UnitTests.Specifications.UserSpecification.ReturnBookSpecifications
{
    public sealed class WhenUserIsBorrowingADifferentBook : SpecificationFor<ReturnBook>
    {
        protected override IEnumerable<IDomainEvent> WhenGiven()
        {
            yield return JohnDoeUserAdded;
            yield return JohnDoeBorrowedWarAndPeace1;
        }
        
        protected override ReturnBook AfterExecuting => new ReturnBook(WarAndPeace2Id, JohnDoeId);

        protected override Func<ReturnBook, Task<Result>> By() => UserCommandExecutorsWith(Repository);

        [Fact]
        public void returns_failure() => Result.IsFailure.Should().BeTrue();

        [Fact]
        public void borrow_is_not_finished() => ProducedEvents.Should().NotContain(EventOf<UserFinishedBookBorrow>());
    }
}