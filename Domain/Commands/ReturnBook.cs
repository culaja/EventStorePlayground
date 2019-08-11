namespace Domain.Commands
{
    public sealed class ReturnBook : SagaCommand
    {
        public BookId BookId { get; }
        public UserId UserId { get; }

        public ReturnBook(
            BookId bookId,
            UserId userId)
        {
            BookId = bookId;
            UserId = userId;
        }
    }
}