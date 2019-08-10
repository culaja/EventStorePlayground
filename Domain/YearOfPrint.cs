using System.Collections.Generic;
using Common;

namespace Domain
{
    public sealed class YearOfPrint : ValueObject<YearOfPrint>
    {
        private readonly int _year;

        private YearOfPrint(int year)
        {
            _year = year;
        }
        
        public static YearOfPrint YearOfPrintFrom(int year) => new YearOfPrint(year);
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _year;
        }

        public static implicit operator int(YearOfPrint yearOfPrint) => yearOfPrint._year;

        public override string ToString() => _year.ToString();
    }
}