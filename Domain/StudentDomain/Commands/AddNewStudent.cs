using System;
using Common;
using Common.Commanding;

namespace Domain.StudentDomain.Commands
{
    public sealed class AddNewStudent : INewAggregateRootCommand<Student>
    {
        public EmailAddress EmailAddress { get; }
        public Name Name { get; }
        public Maybe<City> MaybeCity { get; }
        public bool IsEmployed { get; }

        public AddNewStudent(
            EmailAddress emailAddress,
            Name name,
            Maybe<City> maybeCity,
            bool isEmployed)
        {
            EmailAddress = emailAddress;
            Name = name;
            MaybeCity = maybeCity;
            IsEmployed = isEmployed;
        }

        public Student CreateNew()
        {
            return AggregateRoot.CreateNewFrom<Student>(
                Guid.NewGuid(),
                EmailAddress,
                Name,
                MaybeCity,
                IsEmployed);
        }
    }
}