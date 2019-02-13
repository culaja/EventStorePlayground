using System.Collections.Generic;
using Common;

namespace RabbitMqMessageBus
{
    public sealed class RabbitMqServerConfiguration : ValueObject<RabbitMqServerConfiguration>
    {
        public string HostName { get; }

        public RabbitMqServerConfiguration(
            string hostName)
        {
            HostName = hostName;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return HostName;
        }
    }
}