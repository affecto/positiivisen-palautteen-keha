using System;
using System.Configuration;
using Affecto.PositiveFeedback.Application;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Affecto.PositiveFeedback.Store.MongoDb
{
    internal class FeedbackRepository : IFeedbackRepository
    {
        private readonly IMongoCollection<BsonDocument> employees;
        
        public FeedbackRepository()
        {
            MongoUrl mongourl = new MongoUrl(ConfigurationManager.ConnectionStrings["MongoDB"].ConnectionString);
            IMongoClient client = new MongoClient(mongourl);
            IMongoDatabase database = client.GetDatabase(mongourl.DatabaseName);
            employees = database.GetCollection<BsonDocument>("Employee");
        }
        
        public bool HasEmployee(Guid id)
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            return employees.Find(filter).Any();
        }

        public void AddEmployee(Guid userId, string name)
        {
            var document = new BsonDocument
            {
                {"_id", userId },
                {"Name", name }
            };
            employees.InsertOne(document);
        }

        public void UpdateEmployee(Guid id, string name)
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            UpdateDefinition<BsonDocument> update = Builders<BsonDocument>.Update.Set("Name", name);
            employees.UpdateOne(filter, update);
        }
    }
}
