using System;

namespace Domain
{
    public sealed class InvalidEmailAddressException : Exception
    {
        public string InvalidEmailAddressString { get; }

        public InvalidEmailAddressException(string invalidEmailAddressString) : base($"This is invalid email address string: {invalidEmailAddressString}.")
        {
            InvalidEmailAddressString = invalidEmailAddressString;
        }
    }
}