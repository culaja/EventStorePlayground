using RabbitMQ.Client;

namespace RabbitMqAdapter
{
    internal static class RabbitMqConnectionProvider
    {
        public static IConnection OpenRabbitMqConnection(string hostName)
        {
            var factory = new ConnectionFactory {HostName = hostName};
            return factory.CreateConnection();
        }
    }
}