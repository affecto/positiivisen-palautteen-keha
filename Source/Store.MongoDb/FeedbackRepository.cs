using System;
using System.Collections.Generic;
using System.Linq;
using Affecto.PositiveFeedback.Application;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Affecto.PositiveFeedback.Store.MongoDb
{
    internal class FeedbackRepository : IFeedbackRepository
    {
        private readonly IMongoCollection<BsonDocument> employees;
        
        public FeedbackRepository(ICollections databaseCollections)
        {
            if (databaseCollections == null)
            {
                throw new ArgumentNullException(nameof(databaseCollections));
            }
            employees = databaseCollections.Load("Employee");
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

        public IEnumerable<Employee> GetEmployees()
        {
            return employees.FindSync(FilterDefinition<BsonDocument>.Empty).ToList().Select(doc => new Employee(doc["_id"].AsGuid, doc["Name"].AsString));
        }
    }
}
