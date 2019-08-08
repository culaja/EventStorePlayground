using System.Collections.Generic;
using Common;

namespace Domain.Book
{
    public sealed class BookId : AggregateId
    {
        private readonly string _id;

        private BookId(string id)
        {
            _id = id;
        }
        
        public static BookId BookIdFrom(string name) => new BookId(name);
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _id;
        }

        public override string ToString() => _id;

        public static implicit operator string(BookId bookId) => bookId.ToString();
    }
}