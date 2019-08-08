using Common.Messaging;
using Domain.Book;

namespace Domain.Commands
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