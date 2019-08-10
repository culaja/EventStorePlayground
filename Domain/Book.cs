using Common;
using LibraryEvents.BookEvents;
using static Common.Result;

namespace Domain
{
    public sealed class Book : AggregateRoot
    {
        private YearOfPrint _yearOfPrint;
        private BookName _name;

        private bool _isLend = false;

        public static Book NewBookFrom(BookId id, BookName bookName, YearOfPrint yearOfPrint) => 
            (Book)new Book().ApplyChange(new BookAdded(id, bookName, yearOfPrint));

        private void Apply(BookAdded e)
        {
            SetAggregateId(e.AggregateId.ToBookId());
            _name = e.Name.ToBookName();
            _yearOfPrint = e.YearOfPrint.ToYearOfPrint();
        }
        
        public override string ToString() => $"{Id}: {_yearOfPrint}";

        public Result<Book> LendTo(UserId borrowerUserId)
        {
            if (_isLend) return Fail<Book>($"Book {Id} is already lend.");
            
            ApplyChange(new BookLendToUser(Id, _name, borrowerUserId));
            return Ok(this);
        }

        private void Apply(BookLendToUser e)
        {
            _isLend = true;
        }
    }
}