using Common;

namespace Domain
{
    public sealed class InvalidBookNameException : BadRequestException
    {
        public InvalidBookNameException(string message) : base(message)
        {
        }
    }
}