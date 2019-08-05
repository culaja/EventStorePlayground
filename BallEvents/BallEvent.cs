using Common.Messaging;

namespace BallEvents
{
    public abstract class BallEvent : DomainEvent
    {
        protected BallEvent() : base("Ball")
        {
        }
    }
}