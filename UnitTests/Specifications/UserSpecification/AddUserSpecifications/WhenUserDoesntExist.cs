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
        protected override IEnumerable<IDomainEvent> WhenGiven()
        {
            yield break;
        }
        
        protected override AddUser AfterExecutingCommand => new AddUser(JohnDoeId, JohnDoeFullName);

        protected override Func<AddUser, Task<Result>> Through() => UserCommandExecutorsWith(Repository);

        [Fact]
        public void returns_failure() => Result.IsSuccess.Should().BeTrue();

        [Fact]
        public void user_added() => ProducedEvents.Should().Contain(JohnDoeUserAdded);
    }
}