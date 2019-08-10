using Common;

namespace Domain
{
    public sealed class UserId : AggregateId
    {
        private UserId(string id) : base(nameof(User), id)
        {
        }

        public static UserId UserIdFrom(string id) => new UserId(id);

        public static implicit operator string(UserId userId) => userId.ToString();
    }
}