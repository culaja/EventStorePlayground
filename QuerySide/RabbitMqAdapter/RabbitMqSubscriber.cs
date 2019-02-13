using System;
using Ports;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.Common;

namespace RabbitMqAdapter
{
    public sealed class RabbitMqSubscriber : IRemoteEventSubscriber
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMqSubscriber(string hostName)
        {
            _connection = RabbitMqConnectionProvider.OpenRabbitMqConnection(hostName);
            _channel = _connection.CreateModel();
        }
        
        public IRemoteEventSubscriber Register<T>(Action<SharedEvent> messageReceivedHandler) where T : IAggregateEventSubscription, new()
        {
            var aggregateEventSubscription = new T();
            _channel.ExchangeDeclare(aggregateEventSubscription.AggregateTopicName, "topic", true);
            var queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queueName, aggregateEventSubscription.AggregateTopicName, "");
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var message = ea.Body;
                messageReceivedHandler(message.DeserializeArray());
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