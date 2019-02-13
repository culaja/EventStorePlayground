namespace WebApp.Controllers
{
    public sealed class MoveToDto
    {
        public string EmailAddress { get; }
        public string City { get; }

        public MoveToDto(
            string emailAddress,
            string city)
        {
            EmailAddress = emailAddress;
            City = city;
        }
    }
}