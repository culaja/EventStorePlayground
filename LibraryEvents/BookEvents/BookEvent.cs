using Common.Messaging;

namespace LibraryEvents.BookEvents
{
    public abstract class BookEvent : DomainEvent
    {
        protected BookEvent(string aggregateId) : base("Book", aggregateId)
        {
        }
    }
}