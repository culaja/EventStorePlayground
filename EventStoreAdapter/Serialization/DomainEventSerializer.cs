using System;
using System.Text;
using Common.Messaging;
using EventStore.ClientAPI;
using Newtonsoft.Json;

namespace EventStoreAdapter.Serialization
{
    public static class EventSerializer
    {
        public static EventData Serialize(this IDomainEvent e) =>
            new EventData(
                Guid.NewGuid(), 
                e.GetType().FullName,
                true,
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(e)),
                null);

        public static IDomainEvent Deserialize(this ResolvedEvent resolvedEvent)
        {
            var serializedEventString = Encoding.UTF8.GetString(resolvedEvent.Event.Data);
            try
            {
                return (IDomainEvent)JsonConvert
                    .DeserializeObject(
                        serializedEventString,
                        Type.GetType(resolvedEvent.Event.EventType));
            }
            catch (Exception ex)
            {
                throw new EventDeserializationException(
                    resolvedEvent.Event.EventType,
                    serializedEventString,
                    resolvedEvent.Event.EventNumber,
                    ex);
            }
        }
    }
}