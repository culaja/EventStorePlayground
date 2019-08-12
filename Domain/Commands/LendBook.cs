using Common.Messaging;

namespace Domain.Commands
{
    public sealed class LendBook : SagaCommand
    {
        public BookId BookId { get; }
        public UserId UserId { get; }

        public LendBook(
            BookId bookId,
            UserId userId)
        {
            BookId = bookId;
            UserId = userId;
        }

        public ReturnBook ToReturnBook() =>
            new ReturnBook(
                BookId,
                UserId);
    }
}