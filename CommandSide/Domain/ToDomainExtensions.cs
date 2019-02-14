using Common;
using static Domain.City;
using static Domain.EmailAddress;
using static Domain.Name;

namespace Domain
{
    public static class ToDomainExtensions
    {
        public static Name ToName(this string str) => NameFrom(str);
        
        public static EmailAddress ToEmailAddress(this string str) => EmailAddressFrom(str);
        
        public static City ToCity(this string str) => CityFrom(str);
    }

    public static class ToBaseTypesExtensions
    {
        public static Maybe<string> ToMaybeString(this Maybe<City> maybeCity) =>
            maybeCity.Map(c => c.ToString());
    }
}