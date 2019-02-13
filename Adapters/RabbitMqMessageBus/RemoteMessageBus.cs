using System;
using Common;
using Common.Messaging;
using Ports.Messaging;
using RabbitMqMessageBus.Mappings;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using static System.String;
using static RabbitMqMessageBus.RabbitMqConnectionProvider;

namespace RabbitMqMessageBus
{
    public sealed class RemoteMessageBus : IRemoteMessageBus, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RemoteMessageBus(RabbitMqServerConfiguration rabbitMqServerConfiguration)
        {
            _connection = OpenRabbitMqConnection(rabbitMqServerConfiguration);
            _channel = _connection.CreateModel();
        }
        
        public IDomainEvent Send(IDomainEvent e)
        {
            _channel.ExchangeDeclare(e.AggregateRootType.FullName, "topic", true);
            _channel.BasicPublish(
                e.AggregateRootType.FullName,
                Empty,
                null,
                e.Serialize());
            return e;
        }

        public IRemoteMessageBus SubscribeTo<T, TK>(Action<TK> messageReceivedHandler)
            where T : AggregateRoot
            where TK : IDomainEvent
        {
            _channel.ExchangeDeclare(typeof(T).FullName, "topic", true);
            var queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queueName, typeof(T).FullName, "");
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var message = ea.Body;
                message.Deserialize()
                    .OnSuccess(domainEvent => messageReceivedHandler((TK)domainEvent))
                    .OnFailure(error => Console.Write($"Error deserializing received message: {error}"));
            };

            _channel.BasicConsume(queueName, true, consumer);

            return this;
        }

        public void Dispose()
        {
            _channel.Close();
            _channel.Dispose();
            _connection.Close();
            _connection.Dispose();
        }
    }
}