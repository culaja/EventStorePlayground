using System.Collections.Generic;

namespace LibraryEvents.UserEvents
{
    [ToString]
    public sealed class UserFinishedBookBorrow : UserEvent
    {
        public string UserFullName { get; }
        public string BookId { get; }

        public UserFinishedBookBorrow(
            string userId,
            string userFullName,
            string bookId) : base(userId)
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