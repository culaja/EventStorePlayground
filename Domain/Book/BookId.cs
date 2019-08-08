using Common;

namespace Domain.Book
{
    public sealed class BookId : AggregateId
    {
        private BookId(string id) : base(nameof(Book), id)
        {
        }

        public static BookId BookIdFrom(string name) => new BookId(name);

        public static implicit operator string(BookId bookId) => bookId.ToString();
    }
}