using MongoDB.Bson;
using MongoDB.Driver;

namespace Affecto.PositiveFeedback.Store.MongoDb
{
    public interface ICollections
    {
        IMongoCollection<BsonDocument> Load(string name);
    }
}