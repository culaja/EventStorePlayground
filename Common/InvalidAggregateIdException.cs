namespace Common
{
    public class InvalidAggregateIdException : BadRequestException
    {
        public InvalidAggregateIdException(string message) : base(message)
        {
        }
    }
}