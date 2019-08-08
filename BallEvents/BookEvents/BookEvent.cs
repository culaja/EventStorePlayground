using Common.Messaging;

namespace BallEvents.BookEvents
{
    public abstract class BookEvent : DomainEvent
    {
        protected BookEvent() : base("Book")
        {
        }
    }
}