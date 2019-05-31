namespace Domain.Commands
{
    public sealed class CreateBall : BallCommand
    {
        public int Size { get; }

        public CreateBall(BallId ballId, int size) : base(ballId)
        {
            Size = size;
        }
    }
}