using System;
using System.Text;
using Common;
using Common.Messaging;
using Common.Messaging.Serialization;
using Ports;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

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
        
        public IRemoteEventSubscriber Register<T>(Action<IDomainEvent> messageReceivedHandler) where T : IAggregateEventSubscription, new()
        {
            var aggregateEventSubscription = new T();
            _channel.ExchangeDeclare(aggregateEventSubscription.AggregateTopicName, "topic", true);
            var queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queueName, aggregateEventSubscription.AggregateTopicName, "");
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var messageAsString = Encoding.ASCII.GetString(ea.Body);
                messageAsString.Deserialize()
                    .OnSuccess(message => messageReceivedHandler(message as IDomainEvent))
                    .OnFailure(error => Console.WriteLine($"Failed to deserialize received message: {error}"));
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