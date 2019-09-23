using System.Text.RegularExpressions;
using Common;

namespace Domain
{
    public sealed class UserId : AggregateId
    {
        private static readonly Regex EmailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        
        private UserId(string id) : base(nameof(User), id)
        {
        }

        public static UserId UserIdFrom(Maybe<string> maybeId) => maybeId
            .IfNullOrWhitespace(() => throw new InvalidUserIdException($"{nameof(UserId)} can't be null, empty or white space string."))
            .Map(id => EmailRegex.Match(id).Success ? id : throw new InvalidUserIdException($"{nameof(UserId)} has to be valid e-mail address."))
            .Map(email => new UserId(email)).Value;

        public static implicit operator string(UserId userId) => userId.ToString();
    }
}