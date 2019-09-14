using System.Collections.Generic;

namespace LibraryEvents.BookEvents
{
    [ToString]
    public sealed class BookLentToUser : BookEvent
    {
        public string BookName { get; }
        public string UserId { get; }

        public BookLentToUser(
            string aggregateId,
            string bookName,
            string userId) : base(aggregateId)
        {
            BookName = bookName;
            UserId = userId;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var item in base.GetEqualityComponents()) yield return item;
            yield return BookName;
            yield return UserId;
        }
    }
}