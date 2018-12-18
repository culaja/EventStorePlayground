using System;
using System.Collections.Generic;
using Common;

namespace Domain
{
    public sealed class City : ValueObject<City>
    {
        public static City NoviSad = new City("Novi Sad");
        public static City Belgrade = new City("Belgrade");
        
        private readonly string _name;

        public City(string name)
        {
            _name = name;
        }
        
        public static City CityFrom(Maybe<string> maybeName) => maybeName
            .Ensure(m => m.HasValue, () => throw new ArgumentNullException($"{nameof(City)} can't be empty.", nameof(maybeName)))
            .Ensure(m => !string.IsNullOrWhiteSpace(m.Value), () => throw new ArgumentNullException($"{nameof(City)} can't be empty.", nameof(maybeName)))
            .Map(name => new City(name))
            .Value;

        public override string ToString() => _name;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _name;
        }
    }
}