using Common.Messaging;

namespace LibraryEvents.BookEvents
{
    public abstract class BookEvent : DomainEvent
    {
        protected BookEvent() : base("Book")
        {
        }
    }
}