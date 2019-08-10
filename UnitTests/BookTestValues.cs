using Domain.Book;
using LibraryEvents.BookEvents;
using static Domain.Book.BookId;
using static Domain.Book.BookName;
using static Domain.Book.YearOfPrint;

namespace UnitTests
{
    public static class BookTestValues
    {
        public static readonly BookName WarAndPeaceName = BookNameFrom("War and Peace");
        public static readonly YearOfPrint YearOfPrint2010 = YearOfPrintFrom(2010);
        
        public static readonly BookId WarAndPeace1Id = BookIdFrom(nameof(WarAndPeace1Id));
        public static readonly BookAdded WarAndPeace1Added = new BookAdded(WarAndPeace1Id, WarAndPeaceName, YearOfPrint2010);
        
        public static readonly BookId WarAndPeace2Id = BookIdFrom(nameof(WarAndPeace2Id));
        public static readonly BookAdded WarAndPeace2Added = new BookAdded(WarAndPeace2Id, WarAndPeaceName, YearOfPrint2010);
    }
}