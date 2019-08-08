using Common.Messaging;

namespace Domain.Book.Commands
{
    public abstract class BookCommand : ICommand
    {
        public BookId BookId { get; }

        protected BookCommand(BookId bookId)
        {
            BookId = bookId;
        }

        public override string ToString() => $"{GetType().Name}: {BookId}";
    }
}