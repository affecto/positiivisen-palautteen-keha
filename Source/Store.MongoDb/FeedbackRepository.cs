using System;
using System.Configuration;
using Affecto.PositiveFeedback.Application;
using MongoDB.Bson;
using MongoDB.Driver;


namespace Affecto.PositiveFeedback.Store.MongoDb
{
    internal class FeedbackRepository : IFeedbackRepository
    {
        protected static IMongoClient client;
        protected static IMongoCollection<BsonDocument> collection;
        
        public FeedbackRepository()
        {
            MongoUrl mongourl = new MongoUrl(ConfigurationManager.ConnectionStrings["MongoDB"].ConnectionString);
            client = new MongoClient(mongourl);
            IMongoDatabase database = client.GetDatabase(mongourl.DatabaseName);
            collection = database.GetCollection<BsonDocument>("Employee");
        }
        
        public bool HasEmployee(Guid id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            BsonDocument results = collection.Find(filter).FirstOrDefault();
            return results != null;
        }

        public void AddEmployee(Guid userId, string name)
        {
            var document = new BsonDocument
            {
                {"_id", userId },
                {"Name", name }
            };
            collection.InsertOne(document);
        }

        public void UpdateEmployee(Guid id, string name)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            var update = Builders<BsonDocument>.Update
                .Set("Name", name);
            collection.UpdateOne(filter, update);
        }
    }
}
