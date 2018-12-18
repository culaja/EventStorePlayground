namespace WebApp.Controllers
{
    public sealed class NewStudentDto
    {
        public string EmailAddress { get; }
        public string Name { get; }
        public string City { get; }
        public bool IsEmployed { get; }

        public NewStudentDto(string city, bool isEmployed, string emailAddress, string name)
        {
            City = city;
            IsEmployed = isEmployed;
            EmailAddress = emailAddress;
            Name = name;
        }
    }
}