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
        protected override IReadOnlyList<IDomainEvent> WhenGiven => Events(
            JohnDoeUserIsAdded);
        
        protected override LendBook AfterExecuting => new LendBook(WarAndPeace1Id, JohnDoeId);

        protected override Func<LendBook, Task<Result>> By() => UserCommandExecutorsWith(Repository);
        
        protected override IReadOnlyList<Action> Outcome => Is(
            () => Result.IsSuccess.Should().BeTrue(),
            () => ProducedEvents.Should().Contain(JohnDoeBorrowedWarAndPeace1));
    }
}