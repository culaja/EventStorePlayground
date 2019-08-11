using System.Collections.Generic;

namespace LibraryEvents.BookEvents
{
    public sealed class BookReturned : BookEvent
    {
        public string BookName { get; }
        public string UserId { get; }

        public BookReturned(
            string bookId,
            string bookName,
            string userId) : base(bookId)
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