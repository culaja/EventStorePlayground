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
    public sealed class WhenUserHasNotLentBook : SpecificationFor<LendBook>
    {
        protected override IEnumerable<IDomainEvent> WhenGiven()
        {
            yield return JohnDoeUserAdded;
        }
        
        protected override LendBook AfterExecutingCommand => new LendBook(WarAndPeace1Id, JohnDoeId);

        protected override Func<LendBook, Task<Result>> Through() => UserCommandExecutorsWith(Repository);

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();

        [Fact]
        public void user_borrowed_book() => ProducedEvents.Should().Contain(JohnDoeBorrowedWarAndPeace1);
    }
}