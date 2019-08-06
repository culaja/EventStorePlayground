using System;
using System.Net;
using Common;
using EventStore.ClientAPI.Projections;

namespace EventStoreAdapter.Reading
{
    internal sealed class AggregateTypeProjectionCreator
    {
        private readonly string _eventStoreName;
        private readonly ProjectionsManager _projectionManager;

        public AggregateTypeProjectionCreator(string connectionString, string eventStoreName)
        {
            _eventStoreName = eventStoreName;
            _projectionManager = new ProjectionsManager(NoLogger.NewNoLogger, IpEndPointFromConnectionString(connectionString), TimeSpan.FromSeconds(10));
        }
        
        private static IPEndPoint IpEndPointFromConnectionString(string connectionString)
        {
            var parts = connectionString.Split(new[] {"://", ":"}, 3, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 3 ||
                !IPAddress.TryParse(parts[1], out var ipAddress) ||
                !int.TryParse(parts[2], out var port))
            {
                throw new ArgumentException($"Invalid EventStore connection string passed: {connectionString}");
            }
            
            return new IPEndPoint(ipAddress, port);
        }

        public void CreateAggregateTypeProjectionFor<T>() where T : AggregateRoot
        {
            var state = _projectionManager.GetStateAsync(AggregateTypeProjection<T>.AggregateTypeProjectionFor(_eventStoreName).ProjectionName).Result;
        }
    }
}