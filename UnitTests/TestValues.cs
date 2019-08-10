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
        
        public static readonly BookAdded WarAndPeace1Added = new BookAdded(WarAndPeace1Id, WarAndPeaceName, YearOfPrint2010);
        public static readonly BookLentToUser WarAndPeaceLentToJohnDoe = new BookLentToUser(WarAndPeace1Id, WarAndPeaceName, JohnDoeId);
        
        public static readonly BookAdded WarAndPeace2Added = new BookAdded(WarAndPeace2Id, WarAndPeaceName, YearOfPrint2010);
        
        public static readonly UserAdded JohnDoeUserAdded = new UserAdded(JohnDoeId, JohnDoeFullName);
    }
}