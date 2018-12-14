using System.Net;
using EventStore.ClientAPI;

namespace EventStore
{
    public static class EventStoreConnectionKeeper
    {
        static EventStoreConnectionKeeper()
        {
            KeptConnection = EventStoreConnection
                .Create(new IPEndPoint(IPAddress.Loopback, 1113));
            KeptConnection.ConnectAsync().Wait();
        }
        
        public static IEventStoreConnection KeptConnection { get; }
    }
}