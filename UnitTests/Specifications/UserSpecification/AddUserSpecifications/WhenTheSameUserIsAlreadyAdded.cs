using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using Domain.Commands;
using FluentAssertions;
using Xunit;
using static DomainServices.DomainCommandExecutors;
using static UnitTests.TestValues;

namespace UnitTests.Specifications.UserSpecification.AddUserSpecifications
{
    public sealed class WhenTheSameUserIsAlreadyAdded : Specification<AddUser>
    {
        protected override AddUser CommandToExecute => new AddUser(JohnDoeId, JohnDoeFullName);
        
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return JohnDoeUserAdded;
        }

        protected override Func<AddUser, Task<Result>> When() => CommandExecutorsWith(Repository);

        [Fact]
        public void returns_failure() => Result.IsFailure.Should().BeTrue();
    }
}