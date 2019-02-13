using MongoDB.Driver;

namespace MongoDbEventStore
{
    public sealed class DatabaseContext
    {
        private readonly IMongoDatabase _maintenanceDatabase;

        public DatabaseContext(string connectionString, string databaseName)
        {
            var mongoClient = new MongoClient(connectionString);
            _maintenanceDatabase = mongoClient.GetDatabase(databaseName);
        }

        public IMongoCollection<T> GetCollectionFor<T>()
            => _maintenanceDatabase.GetCollection<T>(typeof(T).Name);
    }
}