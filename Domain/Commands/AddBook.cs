using Domain.Book;

namespace Domain.Commands
{
    public sealed class AddBook : BookCommand
    {
        public BookName BookName { get; }
        public YearOfPrint YearOfPrint { get; }

        public AddBook(
            BookId bookId,
            BookName bookName,
            YearOfPrint yearOfPrint) : base(bookId)
        {
            BookName = bookName;
            YearOfPrint = yearOfPrint;
        }
    }
}