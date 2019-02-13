using System.Text;
using Newtonsoft.Json;
using Shared.Common;

namespace RabbitMqAdapter
{
    public static class NewtonSerializer
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All
        };
        
        public static SharedEvent DeserializeArray(this byte[] array)
        {
            var domainEventDto = (SharedEvent)JsonConvert.DeserializeObject(
                Encoding.ASCII.GetString(array),
                Settings);

            return domainEventDto;
        }
    }
}