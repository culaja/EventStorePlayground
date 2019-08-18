using Common;

namespace Domain
{
    public sealed class UserId : AggregateId
    {
        private UserId(string id) : base(nameof(User), id)
        {
        }

        public static UserId UserIdFrom(Maybe<string> maybeId) => maybeId
            .IfNullOrWhitespace(() => throw new InvalidUserIdException($"{nameof(UserId)} can't be null, empty or white space string."))
            .Map(id => new UserId(id)).Value;

        public static implicit operator string(UserId userId) => userId.ToString();
    }
}