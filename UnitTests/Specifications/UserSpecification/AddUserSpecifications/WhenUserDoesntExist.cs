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

namespace UnitTests.Specifications.UserSpecification.AddUserSpecifications
{
    public sealed class WhenUserDoesntExist : SpecificationFor<AddUser>
    {
        protected override IReadOnlyList<IDomainEvent> WhenGiven => NoEvents;
        
        protected override AddUser AfterExecuting => new AddUser(JohnDoeId, JohnDoeFullName);

        protected override Func<AddUser, Task<Result>> By() => UserCommandExecutorsWith(Repository);

        protected override IReadOnlyList<Action> Outcome => Is(
            () => Result.IsSuccess.Should().BeTrue(),
            () => ProducedEvents.Should().Contain(JohnDoeUserAdded));
    }
}