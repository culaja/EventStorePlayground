using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using Domain.Commands;
using FluentAssertions;
using Xunit;
using static DomainServices.UserCommandExecutors;
using static UnitTests.TestValues;

namespace UnitTests.Specifications.UserSpecification.LendBookSpecifications
{
    public sealed class WhenUserHasNotLentBook : Specification<LendBook>
    {
        protected override LendBook CommandToExecute => new LendBook(WarAndPeace1Id, JohnDoeId);
        
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return JohnDoeUserAdded;
        }

        protected override Func<LendBook, Task<Result>> When() => UserCommandExecutorsWith(Repository);

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();

        [Fact]
        public void user_borrowed_book() => ProducedEvents.Should().Contain(JohnDoeBorrowedWarAndPeace1);
    }
}