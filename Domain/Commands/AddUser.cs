namespace Domain.Commands
{
    public sealed class AddUser : UserCommand
    {
        public FullName FullName { get; }

        public AddUser(
            UserId userId,
            FullName fullName) : base(userId)
        {
            FullName = fullName;
        }
    }
}