using Common;
using LibraryEvents.UserEvents;

namespace Domain
{
    public sealed class User : AggregateRoot
    {
        private FullName _fullName;

        public static User NewUserFrom(UserId userId, FullName fullName) =>
            (User)new User().ApplyChange(new UserAdded(userId, fullName));

        private void Apply(UserAdded e)
        {
            SetAggregateId(e.AggregateId.ToUserId());
            _fullName = e.FullName.ToFullName();
        }
    }
}