using System.Text;
using Common.Eventing;
using Newtonsoft.Json;
using static Newtonsoft.Json.JsonConvert;

namespace EventStore
{
    public static class DomainEventSerializer
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All
        };
        
        public static byte[] Serialize(IDomainEvent domainEvent) => Encoding.ASCII.GetBytes(
            SerializeObject(domainEvent, Settings));

        public static IDomainEvent DeserializeToDomainEvent(byte[] serializedEvent)
        {
            var data = DeserializeObject(
                Encoding.ASCII.GetString(serializedEvent),
                Settings);

            return (IDomainEvent) data;
        }
    }
}