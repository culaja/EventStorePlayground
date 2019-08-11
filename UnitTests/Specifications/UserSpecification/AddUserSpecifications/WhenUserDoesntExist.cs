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
    public sealed class WhenUserDoesntExist : Specification<AddUser>
    {
        protected override AddUser CommandToExecute => new AddUser(JohnDoeId, JohnDoeFullName);
        
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield break;
        }

        protected override Func<AddUser, Task<Result>> When() => UserCommandExecutorsWith(Repository);

        [Fact]
        public void returns_failure() => Result.IsSuccess.Should().BeTrue();

        [Fact]
        public void user_added() => ProducedEvents.Should().Contain(JohnDoeUserAdded);
    }
}