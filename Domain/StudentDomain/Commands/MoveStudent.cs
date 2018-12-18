using Common.Commanding;
using Domain.StudentDomain.Specifications;

namespace Domain.StudentDomain.Commands
{
    public sealed class MoveStudent : IAggregateRootCommand<Student>
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