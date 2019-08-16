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
    public sealed class WhenTheSameUserIsAlreadyAdded : SpecificationFor<AddUser>
    {
        protected override IEnumerable<IDomainEvent> WhenGiven()
        {
            yield return JohnDoeUserAdded;
        }
        
        protected override AddUser AfterExecuting => new AddUser(JohnDoeId, JohnDoeFullName);

        protected override Func<AddUser, Task<Result>> By() => UserCommandExecutorsWith(Repository);

        protected override IReadOnlyList<Action> Outcome => Is(
            () => Result.IsFailure.Should().BeTrue());
    }
}