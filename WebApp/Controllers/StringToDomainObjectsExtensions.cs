using Common;
using Domain;
using static Domain.City;
using static Domain.EmailAddress;
using static Domain.Name;

namespace WebApp.Controllers
{
    public static class StringToDomainObjectsExtensions
    {
        public static EmailAddress ToEmailAddress(this string s) => EmailAddressFrom(s);
        
        public static Name ToName(this string s) => NameFrom(s);

        public static Maybe<City> ToMaybeCity(this string s) => Maybe<string>.From(s)
            .Map(v => CityFrom(v));
        
        public static City ToCity(this string s) => CityFrom(s);
    }
}