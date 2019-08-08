using System.Collections.Generic;
using Common.Messaging;

namespace LibraryEvents.BookEvents
{
    public abstract class BookEvent : DomainEvent
    {
        public string Id { get; }

        protected BookEvent(string id) : base("Book")
        {
            Id = id;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var item in base.GetEqualityComponents()) yield return item;
            yield return Id;
        }
    }
}