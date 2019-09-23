using Common;

namespace Domain
{
    public sealed class BookId : AggregateId
    {
        private BookId(string id) : base(nameof(Book), id)
        {
        }

        public static BookId BookIdFrom(Maybe<string> maybeId) => maybeId
            .IfNullOrWhitespace(() => throw new InvalidBookIdException($"{nameof(BookId)} can't be null, empty or white space string."))
            .Map(id => new BookId(id)).Value;

        public static implicit operator string(BookId bookId) => bookId.ToString();
    }
}