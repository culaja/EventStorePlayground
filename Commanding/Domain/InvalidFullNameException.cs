using Common;

namespace Domain
{
    public sealed class InvalidFullNameException : BadRequestException
    {
        public InvalidFullNameException(string message) : base(message)
        {
        }
    }
}