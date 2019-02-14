using System.Text;
using Common.Messaging;
using Newtonsoft.Json;

namespace RabbitMqAdapter
{
    public static class NewtonSerializer
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All
        };
        
        public static IDomainEvent DeserializeArray(this byte[] array)
        {
            var domainEventDto = (IDomainEvent)JsonConvert.DeserializeObject(
                Encoding.ASCII.GetString(array),
                Settings);

            return domainEventDto;
        }
    }
}