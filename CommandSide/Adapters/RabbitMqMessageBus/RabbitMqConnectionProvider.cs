using RabbitMQ.Client;

namespace RabbitMqMessageBus
{
    internal static class RabbitMqConnectionProvider
    {
        public static IConnection OpenRabbitMqConnection(RabbitMqServerConfiguration rabbitMqServerConfiguration)
        {
            var factory = new ConnectionFactory {HostName = rabbitMqServerConfiguration.HostName};
            return factory.CreateConnection();
        }
    }
}