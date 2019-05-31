using Common.Messaging;

namespace Domain.Commands
{
    public abstract class BallCommand : ICommand
    {
        public BallId BallId { get; }

        protected BallCommand(BallId ballId)
        {
            BallId = ballId;
        }

        public override string ToString() => $"{nameof(BallCommand)}: {BallId}";
    }
}