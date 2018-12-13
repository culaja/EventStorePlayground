using System.Collections.Generic;
using Common;

namespace Domain
{
    public sealed class City : ValueObject<City>
    {
        public static City NoviSad = new City("Novi Sad");
        public static City Belgrade = new City("Belgrade");
        
        private readonly string _name;

        private City(string name)
        {
            _name = name;
        }

        public override string ToString() => _name;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _name;
        }
    }
}