using Common.Messaging;
using Newtonsoft.Json;

namespace UnitTests.DomainEventSerializationTests
{
    public static class SerializationUtilities
    {
        public static string Serialize(this IDomainEvent domainEvent) => JsonConvert.SerializeObject(domainEvent);

        public static IDomainEvent Deserialize<T>(this string serializedDomainEvent) where T : IDomainEvent =>
            JsonConvert.DeserializeObject<T>(serializedDomainEvent);
    }
}