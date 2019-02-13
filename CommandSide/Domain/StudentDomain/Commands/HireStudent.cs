using Common.Messaging;

namespace Domain.StudentDomain.Commands
{
    public sealed class HireStudent : ICommand
    {
        public EmailAddress EmailAddress { get; }

        public HireStudent(EmailAddress emailAddress)
        {
            EmailAddress = emailAddress;
        }
    }
}