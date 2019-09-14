using Common;
using LibraryEvents.UserEvents;
using static Common.Maybe<Domain.BookId>;
using static Common.Result;

namespace Domain
{
    public sealed class User : AggregateRoot
    {
        private FullName _fullName;
        
        private Maybe<BookId> _maybeBorrowedBook = None;

        private bool IsBorrowingABook => _maybeBorrowedBook.HasValue;

        public static User NewUserFrom(UserId userId, FullName fullName) =>
            (User)new User().ApplyChange(new UserAdded(userId, fullName));

        private void Apply(UserAdded e)
        {
            SetAggregateId(e.AggregateId.ToUserId());
            _fullName = e.FullName.ToFullName();
        }

        public Result<User> BorrowBook(BookId bookToLendId)
        {
            if (IsBorrowingABook) return Fail<User>($"User {Id} has already borrowed book '{_maybeBorrowedBook.Value}'.");
            
            ApplyChange(new UserBorrowedBook(Id, _fullName, bookToLendId));
            return Ok(this);
        }

        private void Apply(UserBorrowedBook e)
        {
            _maybeBorrowedBook = e.BookId.ToBookId();
        }

        public Result<User> FinishBorrowOf(BookId bookId)
        {
            if (IsBorrowingABook && _maybeBorrowedBook.Value == bookId)
            {
                ApplyChange(new UserFinishedBookBorrow(Id, _fullName, bookId));
                return Ok(this);
            }

            return Fail<User>($"User {Id} is not borrowing a book {bookId}");
        }

        private void Apply(UserFinishedBookBorrow e)
        {
            _maybeBorrowedBook = None;
        }
    }
}