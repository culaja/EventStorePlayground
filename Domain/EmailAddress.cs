using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Common;

namespace Domain
{
    public sealed class EmailAddress : ValueObject<EmailAddress>
    {
        public static EmailAddress NoReplyAtWalletConnector = EmailAddressFrom("no-reply@wallet-connector.com");
        
        private const string MatchEmailAddressPattern =
            "^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";

        internal string EmailAddressAsString { get; }

        public EmailAddress(string emailAddressString)
        {
            EmailAddressAsString = emailAddressString;
        }

        public static EmailAddress EmailAddressFrom(Maybe<string> maybeEmailAddressString) => maybeEmailAddressString
            .Ensure(m => m.HasValue, () => throw new ArgumentNullException($"{nameof(EmailAddress)} can't be empty.", nameof(maybeEmailAddressString)))
            .Ensure(m => Regex.IsMatch(m.Value, MatchEmailAddressPattern), m => throw new InvalidEmailAddressException(m.Value))
            .Map(emailAddressString => new EmailAddress(emailAddressString))
            .Value;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return EmailAddressAsString;
        }

        public override string ToString() => EmailAddressAsString;

        public static implicit operator string(EmailAddress emailAddress) => emailAddress.EmailAddressAsString;

    }
}