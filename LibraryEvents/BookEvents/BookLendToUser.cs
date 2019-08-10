using System.Collections.Generic;

namespace LibraryEvents.BookEvents
{
    [ToString]
    public sealed class BookLendToUser : BookEvent
    {
        public string BookName { get; }
        public string UserId { get; }

        public BookLendToUser(
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