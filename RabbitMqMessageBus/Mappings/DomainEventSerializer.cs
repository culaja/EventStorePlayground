using Common.Messaging;

namespace RabbitMqMessageBus.Mappings
{
    public static class DomainEventSerializer
    {
        public static byte[] Serialize(this IDomainEvent e)
        {
            return new byte[] {};
        }
    }
}