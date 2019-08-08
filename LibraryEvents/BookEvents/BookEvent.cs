using Common.Messaging;

namespace LibraryEvents.BookEvents
{
    public abstract class BookEvent : DomainEvent
    {
        protected BookEvent(string id) : base("Book", id)
        {
        }
    }
}