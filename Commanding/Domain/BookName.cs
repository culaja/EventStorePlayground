using System.Collections.Generic;
using Common;

namespace Domain
{
    public sealed class BookName : ValueObject<BookName>
    {
        private readonly string _name;

        private BookName(string name)
        {
            _name = name;
        }

        public static BookName BookNameFrom(Maybe<string> maybeName) => maybeName
            .IfNullOrWhitespace(() =>
                throw new InvalidBookNameException($"{nameof(BookName)} can't be null, empty or white space string."))
            .Map(name => new BookName(name)).Value;
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _name;
        }

        public override string ToString() => _name;

        public static implicit operator string(BookName bookName) => bookName.ToString();
    }
}