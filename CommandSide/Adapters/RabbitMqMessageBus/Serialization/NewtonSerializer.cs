using System.Text;
using Common;
using Common.Messaging;
using Newtonsoft.Json;

namespace RabbitMqMessageBus.Serialization
{
    public static class NewtonSerializer
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All
        };
        
        public static byte[] Serialize(this IDomainEvent e) => Encoding.ASCII.GetBytes(
            JsonConvert.SerializeObject(e, Settings));

        public static Result<IDomainEvent> DeserializeArray(this byte[] array)
        {
            try
            {
                var domainEventDto = (IDomainEvent)JsonConvert.DeserializeObject(
                    Encoding.ASCII.GetString(array),
                    Settings);

                return domainEventDto.ToOkResult();
            }
            catch (JsonReaderException e)
            {
                return Result.Fail<IDomainEvent>(e.Message);
            }
        }
    }
}