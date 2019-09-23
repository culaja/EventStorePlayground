using Common;

namespace Domain
{
    public sealed class InvalidYearOfPrintException : BadRequestException
    {
        public InvalidYearOfPrintException(string message) : base(message)
        {
        }
    }
}