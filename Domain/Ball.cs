using BallEvents;
using Common;
using static Domain.BallId;

namespace Domain
{
    public class Ball : AggregateRoot
    {
        private int _size = 0;
        private string _ballPosition = "";
        
        private void Apply(BallCreated e)
        {
            SetAggregateId(BallIdFrom(e.Name));
            _size = e.Size;
        }

        private void Apply(BallPassed e)
        {
            _ballPosition = e.PassTo;
        }

        public override string ToString() => $"{Id}: {_size}, {_ballPosition}";
    }
}