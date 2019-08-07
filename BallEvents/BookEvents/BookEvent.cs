using System.Collections.Generic;
using Common.Messaging;

namespace BallEvents.BookEvents
{
    public abstract class BookEvent : DomainEvent
    {
        protected BookEvent() : base("Book")
        {
        }
    }
    
    public sealed class BookCreated
}