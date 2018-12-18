using System;
using Common;
using Common.Commanding;
using Domain.StudentDomain.Specifications;

namespace Domain.StudentDomain.Commands
{
    public sealed class AddNewStudent : INewAggregateCommand
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

    public sealed class MoveStudent : ISpecificationBasedAggregateCommand<Student>
    {
        public EmailAddress EmailAddress { get; }
        public City CityToMoveTo { get; }

        public MoveStudent(
            EmailAddress emailAddress,
            City cityToMoveTo)
        {
            EmailAddress = emailAddress;
            CityToMoveTo = cityToMoveTo;
        }

        public ISpecification<Student> Specification => new StudentByEmailAddressSpecification(EmailAddress);
    }
}