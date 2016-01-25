using System;
using Affecto.Configuration.Extensions;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace Affecto.PositiveFeedback.Store.MongoDb
{
    internal class EmployeeCollection : ICollection<Employee>
    {
        private IMongoDatabase database;

        static EmployeeCollection()
        {
            BsonClassMap.RegisterClassMap<Employee>(cm =>
            {
                cm.AutoMap();
                cm.SetIdMember(cm.GetMemberMap(employee => employee.Id));
            });
        }

        public EmployeeCollection(IApplicationConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            SetupDatabase(configuration);
        }

        public IMongoCollection<Employee> Load()
        {
            return database.GetCollection<Employee>("Employee");
        }

        public IGridFSBucket CreateGridFSBucket()
        {
            return new GridFSBucket(database);
        }

        private void SetupDatabase(IApplicationConfiguration configuration)
        {
            var mongourl = new MongoUrl(configuration.GetConnectionString("MongoDB"));
            var client = new MongoClient(mongourl);
            database = client.GetDatabase(mongourl.DatabaseName);
        }
    }
}