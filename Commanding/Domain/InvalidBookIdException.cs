using Common;

namespace Domain
{
    public sealed class InvalidBookIdException : BadRequestException
    {
        public InvalidBookIdException(string message) : base(message)
        {
        }
    }
}