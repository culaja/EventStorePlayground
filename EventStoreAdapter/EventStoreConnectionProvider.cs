using System.Threading.Tasks;
using Common;
using EventStore.ClientAPI;
using static System.Threading.Tasks.Task;
using static Common.Maybe<EventStore.ClientAPI.IEventStoreConnection>;

namespace EventStoreAdapter
{
    public static class EventStoreConnectionProvider
    {
        private static readonly object SyncObject = new object();
        private static Maybe<IEventStoreConnection> _eventStoreConnectionInstance = None;
        
        public static Task<IEventStoreConnection> GrabSingleEventStoreConnectionFor(string connectionString)
        {
            if (_eventStoreConnectionInstance.HasNoValue)
            {
                lock (SyncObject)
                {
                    if (_eventStoreConnectionInstance.HasNoValue)
                    {
                        _eventStoreConnectionInstance = From(EventStoreConnection.Create(connectionString));
                        return _eventStoreConnectionInstance.Value.ConnectAsync()
                            .ContinueWith(t => _eventStoreConnectionInstance.Value);
                    }
                }
            }

            return FromResult(_eventStoreConnectionInstance.Value);
        }
    }
}