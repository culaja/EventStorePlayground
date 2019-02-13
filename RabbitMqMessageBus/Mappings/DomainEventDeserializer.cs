using Common;
using Common.Messaging;

namespace RabbitMqMessageBus.Mappings
{
    public static class DomainEventDeserializer
    {
        public static Result<IDomainEvent> Deserialize(this byte[] array)
        {
            return Result.Fail<IDomainEvent>("Nothing");
        }
    }
}