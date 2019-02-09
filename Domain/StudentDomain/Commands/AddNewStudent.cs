using System;
using Common;
using Common.Messaging;

namespace Domain.StudentDomain.Commands
{
    public sealed class AddNewStudent : ICommand
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
    }
}