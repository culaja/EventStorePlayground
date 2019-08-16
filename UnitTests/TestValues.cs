using Domain;
using LibraryEvents.BookEvents;
using LibraryEvents.UserEvents;
using static Domain.BookId;
using static Domain.BookName;
using static Domain.FullName;
using static Domain.UserId;
using static Domain.YearOfPrint;

namespace UnitTests
{
    public static class TestValues
    {
        public static readonly BookName WarAndPeaceName = BookNameFrom("War and Peace");
        public static readonly YearOfPrint YearOfPrint2010 = YearOfPrintFrom(2010);
        
        public static readonly BookId WarAndPeace1Id = BookIdFrom(nameof(WarAndPeace1Id));
        
        public static readonly BookId WarAndPeace2Id = BookIdFrom(nameof(WarAndPeace2Id));
        
        public static readonly UserId JohnDoeId = UserIdFrom("johndoe@gmail.com");
        public static readonly FullName JohnDoeFullName = FullNameFrom("John Doe");
        
        public static readonly UserId StankoId = UserIdFrom("culaja@gmail.com");
        public static readonly FullName StankoFullName = FullNameFrom("Stanko Culaja");
        
        public static readonly BookAdded WarAndPeace1IsAdded = new BookAdded(WarAndPeace1Id, WarAndPeaceName, YearOfPrint2010);
        public static readonly BookLentToUser WarAndPeace1IsLentToJohnDoe = new BookLentToUser(WarAndPeace1Id, WarAndPeaceName, JohnDoeId);
        public static readonly BookReturned WarAndPeace1IsReturnedByJohnDoe = new BookReturned(WarAndPeace1Id, WarAndPeaceName, JohnDoeId);
        
        public static readonly BookAdded WarAndPeace2IsAdded = new BookAdded(WarAndPeace2Id, WarAndPeaceName, YearOfPrint2010);
        public static readonly BookLentToUser WarAndPeace2IsLentToJohnDoe = new BookLentToUser(WarAndPeace2Id, WarAndPeaceName, JohnDoeId);
        public static readonly BookReturned WarAndPeace2IsReturnedByJohnDoe = new BookReturned(WarAndPeace2Id, WarAndPeaceName, JohnDoeId);
        
        public static readonly UserAdded JohnDoeUserIsAdded = new UserAdded(JohnDoeId, JohnDoeFullName);
        public static readonly UserBorrowedBook JohnDoeBorrowedWarAndPeace1 = new UserBorrowedBook(JohnDoeId, JohnDoeFullName, WarAndPeace1Id);
        public static readonly UserFinishedBookBorrow JohDoeFinishedWarAndPeace1Borrow = new UserFinishedBookBorrow(JohnDoeId, JohnDoeFullName, WarAndPeace1Id);
        
        public static readonly UserAdded StankoUserIsAdded = new UserAdded(StankoId, StankoFullName);
        
    }
}