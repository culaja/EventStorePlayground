namespace Domain.Book.Commands
{
    public sealed class AddBook : BookCommand
    {
        public YearOfPrint YearOfPrint { get; }

        public AddBook(
            BookId bookId,
            YearOfPrint yearOfPrint) : base(bookId)
        {
            YearOfPrint = yearOfPrint;
        }
    }
}