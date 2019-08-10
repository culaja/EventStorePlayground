using Common.Messaging;

namespace Domain.Commands
{
    public abstract class UserCommand : ICommand
    {
        public UserId UserId { get; }

        public UserCommand(UserId userId)
        {
            UserId = userId;
        }
        
        public override string ToString() => $"{GetType().Name}: {UserId}";
    }
}