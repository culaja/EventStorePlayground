using System.Collections.Generic;
using Common;

namespace Domain
{
    public class BallId : AggregateId
    {
        private readonly string _name;

        private BallId(string name)
        {
            _name = name;
        }
        
        public static BallId BallIdFrom(string name) => new BallId(name);
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _name;
        }

        public override string ToStreamName() => $"Ball_{_name}";

        public override string ToString() => _name;

        public static implicit operator string(BallId ballId) => ballId.ToString();
    }
}