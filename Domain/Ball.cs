using BallEvents;
using Common;
using static Domain.BallId;

namespace Domain
{
    public class Ball : AggregateRoot
    {
        private int _size = 0;
        private string _ballPosition = "";

        public static Ball NewBallWith(BallId id, int size) => 
            (Ball)new Ball().ApplyChange(new BallCreated(id, size));

        private void Apply(BallCreated e)
        {
            SetAggregateId(BallIdFrom(e.Name));
            _size = e.Size;
        }

        public Result<Ball> PassTo(string someone)
        {
            if (someone != _ballPosition)
                ApplyChange(new BallPassed(Id, _ballPosition, someone));
            
            return Result.Ok(this);
        }

        private void Apply(BallPassed e)
        {
            _ballPosition = e.PassTo;
        }

        public override string ToString() => $"{Id}: {_size}, {_ballPosition}";
    }
}