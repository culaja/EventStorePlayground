using Common.Commanding;

namespace Domain.StudentDomain.Commands
{
    public sealed class MoveStudent : IAggregateRootCommand
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
    }
}