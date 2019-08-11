using Common;
using LibraryEvents.UserEvents;
using static Common.Result;

namespace Domain
{
    public sealed class User : AggregateRoot
    {
        private FullName _fullName;
        private bool _hasBorrowedABook = false;

        public static User NewUserFrom(UserId userId, FullName fullName) =>
            (User)new User().ApplyChange(new UserAdded(userId, fullName));

        private void Apply(UserAdded e)
        {
            SetAggregateId(e.AggregateId.ToUserId());
            _fullName = e.FullName.ToFullName();
        }

        public Result<User> BorrowBook(BookId bookToLendId)
        {
            if (_hasBorrowedABook) return Fail<User>($"User {Id} has already borrowed some book.");
            
            ApplyChange(new UserBorrowedBook(Id, _fullName, bookToLendId));
            return Ok(this);
        }

        private void Apply(UserBorrowedBook _)
        {
            _hasBorrowedABook = true;
        }
    }
}