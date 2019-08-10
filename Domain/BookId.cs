using Common;

namespace Domain
{
    public sealed class BookId : AggregateId
    {
        private BookId(string id) : base(nameof(Book), id)
        {
        }

        public static BookId BookIdFrom(string id) => new BookId(id);

        public static implicit operator string(BookId bookId) => bookId.ToString();
    }
}