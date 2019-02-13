using System.Text;
using Common;
using Newtonsoft.Json;
using Shared.Common;

namespace RabbitMqMessageBus.Serialization
{
    public static class NewtonSerializer
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All
        };
        
        public static byte[] Serialize(this SharedEvent e) => Encoding.ASCII.GetBytes(
            JsonConvert.SerializeObject(e, Settings));

        public static Result<SharedEvent> DeserializeArray(this byte[] array)
        {
            try
            {
                var domainEventDto = (SharedEvent)JsonConvert.DeserializeObject(
                    Encoding.ASCII.GetString(array),
                    Settings);

                return domainEventDto.ToOkResult();
            }
            catch (JsonReaderException e)
            {
                return Result.Fail<SharedEvent>(e.Message);
            }
        }
    }
}