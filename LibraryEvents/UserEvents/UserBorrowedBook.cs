using System.Collections.Generic;

namespace LibraryEvents.UserEvents
{
    [ToString]
    public sealed class UserBorrowedBook : UserEvent
    {
        public string UserFullName { get; }
        public string BookId { get; }

        public UserBorrowedBook(
            string aggregateId,
            string userFullName,
            string bookId) : base(aggregateId)
        {
            UserFullName = userFullName;
            BookId = bookId;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var item in base.GetEqualityComponents()) yield return item;
            yield return UserFullName;
            yield return BookId;
        }
    }
}