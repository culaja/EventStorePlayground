using Common.Messaging;

namespace Domain.StudentDomain.Commands
{
    public sealed class MoveStudent : ICommand
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