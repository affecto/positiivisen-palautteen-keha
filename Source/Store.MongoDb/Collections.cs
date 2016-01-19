using Affecto.Configuration.Extensions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Affecto.PositiveFeedback.Store.MongoDb
{
    internal class Collections : ICollections
    {
        private readonly IMongoDatabase database;

        public Collections(IApplicationConfiguration configuration)
        {
            var mongourl = new MongoUrl(configuration.GetConnectionString("MongoDB"));
            var client = new MongoClient(mongourl);
            database = client.GetDatabase(mongourl.DatabaseName);
        }

        public IMongoCollection<BsonDocument> Load(string name)
        {
            return database.GetCollection<BsonDocument>(name);
        }
    }
}
