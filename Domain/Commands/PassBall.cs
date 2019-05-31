namespace Domain.Commands
{
    public sealed class PassBall : BallCommand
    {
        public string Destination { get; }

        public PassBall(BallId ballId, string destination) : base(ballId)
        {
            Destination = destination;
        }
    }
}