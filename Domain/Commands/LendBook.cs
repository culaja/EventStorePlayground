namespace Domain.Commands
{
    public sealed class LendBook : SagaCommand
    {
        public BookId BookToLendId { get; }
        public UserId BorrowerUserId { get; }

        public LendBook(
            BookId bookToLendId,
            UserId borrowerUserId)
        {
            BookToLendId = bookToLendId;
            BorrowerUserId = borrowerUserId;
        }
    }
}