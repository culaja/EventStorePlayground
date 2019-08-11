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
    public sealed class WhenUserIsBorrowingADifferentBook : Specification<ReturnBook>
    {
        protected override ReturnBook CommandToExecute => new ReturnBook(WarAndPeace2Id, JohnDoeId);
        
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return JohnDoeUserAdded;
            yield return JohnDoeBorrowedWarAndPeace1;
        }

        protected override Func<ReturnBook, Task<Result>> When() => UserCommandExecutorsWith(Repository);

        [Fact]
        public void returns_failure() => Result.IsFailure.Should().BeTrue();

        [Fact]
        public void borrow_is_not_finished() => ProducedEvents.Should().NotContain(EventOf<UserFinishedBookBorrow>());
    }
}