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
    public sealed class WhenBorrowOfTheBookIsAlreadyFinished : Specification<ReturnBook>
    {
        protected override ReturnBook CommandToExecute => new ReturnBook(WarAndPeace1Id, JohnDoeId);
        
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return JohnDoeUserAdded;
            yield return JohnDoeBorrowedWarAndPeace1;
            yield return JohDoeFinishedWarAndPeace1Borrow;
        }

        protected override Func<ReturnBook, Task<Result>> When() => UserCommandExecutorsWith(Repository);

        [Fact]
        public void returns_failure() => Result.IsFailure.Should().BeTrue();

        [Fact]
        public void book_is_not_returned() => ProducedEvents.Should().NotContain(EventOf<UserFinishedBookBorrow>());
    }
}