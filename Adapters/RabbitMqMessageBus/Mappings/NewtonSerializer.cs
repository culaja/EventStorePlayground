using System.Text;
using Common;
using Newtonsoft.Json;
using static Newtonsoft.Json.JsonConvert;

namespace RabbitMqMessageBus.Mappings
{
    public static class NewtonSerializer
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All
        };
        
        public static byte[] Serialize(this DomainEventDto e) => Encoding.ASCII.GetBytes(
            SerializeObject(e, Settings));

        public static Result<DomainEventDto> DeserializeArray(this byte[] array)
        {
            try
            {
                var domainEventDto = (DomainEventDto)DeserializeObject(
                    Encoding.ASCII.GetString(array),
                    Settings);

                return domainEventDto.ToOkResult();
            }
            catch (JsonReaderException e)
            {
                return Result.Fail<DomainEventDto>(e.Message);
            }
        }
    }
}