using Common;

namespace Domain
{
    public sealed class InvalidUserIdException : BadRequestException
    {
        public InvalidUserIdException(string message) : base(message)
        {
        }
    }
}