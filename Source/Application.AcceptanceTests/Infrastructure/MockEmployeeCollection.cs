
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Affecto.PositiveFeedback.Application.AcceptanceTests.Infrastructure
{
    internal class MockEmployeeCollection : Store.MongoDb.ICollection<Store.MongoDb.Employee>, IMongoCollection<Store.MongoDb.Employee>
    {
        private readonly List<Store.MongoDb.Employee> employees;

        public MockEmployeeCollection()
        {
            employees = new List<Store.MongoDb.Employee>();
        }

        public IMongoCollection<Store.MongoDb.Employee> Load()
        {
            return this;
        }

        /// <summary>
        /// Runs an aggregation pipeline.
        /// </summary>
        /// <param name="pipeline">The pipeline.</param><param name="options">The options.</param><param name="cancellationToken">The cancellation token.</param><typeparam name="TResult">The type of the result.</typeparam>
        /// <returns>
        /// A cursor.
        /// </returns>
        public IAsyncCursor<TResult> Aggregate<TResult>(PipelineDefinition<Store.MongoDb.Employee, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Runs an aggregation pipeline.
        /// </summary>
        /// <param name="pipeline">The pipeline.</param><param name="options">The options.</param><param name="cancellationToken">The cancellation token.</param><typeparam name="TResult">The type of the result.</typeparam>
        /// <returns>
        /// A Task whose result is a cursor.
        /// </returns>
        public Task<IAsyncCursor<TResult>> AggregateAsync<TResult>(PipelineDefinition<Store.MongoDb.Employee, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Performs multiple write operations.
        /// </summary>
        /// <param name="requests">The requests.</param><param name="options">The options.</param><param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The result of writing.
        /// </returns>
        public BulkWriteResult<Store.MongoDb.Employee> BulkWrite(IEnumerable<WriteModel<Store.MongoDb.Employee>> requests, BulkWriteOptions options = null, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Performs multiple write operations.
        /// </summary>
        /// <param name="requests">The requests.</param><param name="options">The options.</param><param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The result of writing.
        /// </returns>
        public Task<BulkWriteResult<Store.MongoDb.Employee>> BulkWriteAsync(IEnumerable<WriteModel<Store.MongoDb.Employee>> requests, BulkWriteOptions options = null, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Counts the number of documents in the collection.
        /// </summary>
        /// <param name="filter">The filter.</param><param name="options">The options.</param><param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The number of documents in the collection.
        /// </returns>
        public long Count(FilterDefinition<Store.MongoDb.Employee> filter, CountOptions options = null, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Counts the number of documents in the collection.
        /// </summary>
        /// <param name="filter">The filter.</param><param name="options">The options.</param><param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The number of documents in the collection.
        /// </returns>
        public Task<long> CountAsync(FilterDefinition<Store.MongoDb.Employee> filter, CountOptions options = null, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Deletes multiple documents.
        /// </summary>
        /// <param name="filter">The filter.</param><param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The result of the delete operation.
        /// </returns>
        public DeleteResult DeleteMany(FilterDefinition<Store.MongoDb.Employee> filter, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Deletes multiple documents.
        /// </summary>
        /// <param name="filter">The filter.</param><param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The result of the delete operation.
        /// </returns>
        public Task<DeleteResult> DeleteManyAsync(FilterDefinition<Store.MongoDb.Employee> filter, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Deletes a single document.
        /// </summary>
        /// <param name="filter">The filter.</param><param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The result of the delete operation.
        /// </returns>
        public DeleteResult DeleteOne(FilterDefinition<Store.MongoDb.Employee> filter, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Deletes a single document.
        /// </summary>
        /// <param name="filter">The filter.</param><param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The result of the delete operation.
        /// </returns>
        public Task<DeleteResult> DeleteOneAsync(FilterDefinition<Store.MongoDb.Employee> filter, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gets the distinct values for a specified field.
        /// </summary>
        /// <param name="field">The field.</param><param name="filter">The filter.</param><param name="options">The options.</param><param name="cancellationToken">The cancellation token.</param><typeparam name="TField">The type of the result.</typeparam>
        /// <returns>
        /// A cursor.
        /// </returns>
        public IAsyncCursor<TField> Distinct<TField>(FieldDefinition<Store.MongoDb.Employee, TField> field, FilterDefinition<Store.MongoDb.Employee> filter, DistinctOptions options = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gets the distinct values for a specified field.
        /// </summary>
        /// <param name="field">The field.</param><param name="filter">The filter.</param><param name="options">The options.</param><param name="cancellationToken">The cancellation token.</param><typeparam name="TField">The type of the result.</typeparam>
        /// <returns>
        /// A Task whose result is a cursor.
        /// </returns>
        public Task<IAsyncCursor<TField>> DistinctAsync<TField>(FieldDefinition<Store.MongoDb.Employee, TField> field, FilterDefinition<Store.MongoDb.Employee> filter, DistinctOptions options = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Finds the documents matching the filter.
        /// </summary>
        /// <param name="filter">The filter.</param><param name="options">The options.</param><param name="cancellationToken">The cancellation token.</param><typeparam name="TProjection">The type of the projection (same as TDocument if there is no projection).</typeparam>
        /// <returns>
        /// A cursor.
        /// </returns>
        public IAsyncCursor<TProjection> FindSync<TProjection>(FilterDefinition<Store.MongoDb.Employee> filter, FindOptions<Store.MongoDb.Employee, TProjection> options = null, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Finds the documents matching the filter.
        /// </summary>
        /// <param name="filter">The filter.</param><param name="options">The options.</param><param name="cancellationToken">The cancellation token.</param><typeparam name="TProjection">The type of the projection (same as TDocument if there is no projection).</typeparam>
        /// <returns>
        /// A Task whose result is a cursor.
        /// </returns>
        public Task<IAsyncCursor<TProjection>> FindAsync<TProjection>(FilterDefinition<Store.MongoDb.Employee> filter, FindOptions<Store.MongoDb.Employee, TProjection> options = null, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Finds a single document and deletes it atomically.
        /// </summary>
        /// <param name="filter">The filter.</param><param name="options">The options.</param><param name="cancellationToken">The cancellation token.</param><typeparam name="TProjection">The type of the projection (same as TDocument if there is no projection).</typeparam>
        /// <returns>
        /// The returned document.
        /// </returns>
        public TProjection FindOneAndDelete<TProjection>(FilterDefinition<Store.MongoDb.Employee> filter, FindOneAndDeleteOptions<Store.MongoDb.Employee, TProjection> options = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Finds a single document and deletes it atomically.
        /// </summary>
        /// <param name="filter">The filter.</param><param name="options">The options.</param><param name="cancellationToken">The cancellation token.</param><typeparam name="TProjection">The type of the projection (same as TDocument if there is no projection).</typeparam>
        /// <returns>
        /// The returned document.
        /// </returns>
        public Task<TProjection> FindOneAndDeleteAsync<TProjection>(FilterDefinition<Store.MongoDb.Employee> filter, FindOneAndDeleteOptions<Store.MongoDb.Employee, TProjection> options = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Finds a single document and replaces it atomically.
        /// </summary>
        /// <param name="filter">The filter.</param><param name="replacement">The replacement.</param><param name="options">The options.</param><param name="cancellationToken">The cancellation token.</param><typeparam name="TProjection">The type of the projection (same as TDocument if there is no projection).</typeparam>
        /// <returns>
        /// The returned document.
        /// </returns>
        public TProjection FindOneAndReplace<TProjection>(FilterDefinition<Store.MongoDb.Employee> filter, Store.MongoDb.Employee replacement, FindOneAndReplaceOptions<Store.MongoDb.Employee, TProjection> options = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Finds a single document and replaces it atomically.
        /// </summary>
        /// <param name="filter">The filter.</param><param name="replacement">The replacement.</param><param name="options">The options.</param><param name="cancellationToken">The cancellation token.</param><typeparam name="TProjection">The type of the projection (same as TDocument if there is no projection).</typeparam>
        /// <returns>
        /// The returned document.
        /// </returns>
        public Task<TProjection> FindOneAndReplaceAsync<TProjection>(FilterDefinition<Store.MongoDb.Employee> filter, Store.MongoDb.Employee replacement, FindOneAndReplaceOptions<Store.MongoDb.Employee, TProjection> options = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Finds a single document and updates it atomically.
        /// </summary>
        /// <param name="filter">The filter.</param><param name="update">The update.</param><param name="options">The options.</param><param name="cancellationToken">The cancellation token.</param><typeparam name="TProjection">The type of the projection (same as TDocument if there is no projection).</typeparam>
        /// <returns>
        /// The returned document.
        /// </returns>
        public TProjection FindOneAndUpdate<TProjection>(FilterDefinition<Store.MongoDb.Employee> filter, UpdateDefinition<Store.MongoDb.Employee> update, FindOneAndUpdateOptions<Store.MongoDb.Employee, TProjection> options = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Finds a single document and updates it atomically.
        /// </summary>
        /// <param name="filter">The filter.</param><param name="update">The update.</param><param name="options">The options.</param><param name="cancellationToken">The cancellation token.</param><typeparam name="TProjection">The type of the projection (same as TDocument if there is no projection).</typeparam>
        /// <returns>
        /// The returned document.
        /// </returns>
        public Task<TProjection> FindOneAndUpdateAsync<TProjection>(FilterDefinition<Store.MongoDb.Employee> filter, UpdateDefinition<Store.MongoDb.Employee> update, FindOneAndUpdateOptions<Store.MongoDb.Employee, TProjection> options = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Inserts a single document.
        /// </summary>
        /// <param name="document">The document.</param><param name="options">The options.</param><param name="cancellationToken">The cancellation token.</param>
        public void InsertOne(Store.MongoDb.Employee document, InsertOneOptions options = null, CancellationToken cancellationToken = new CancellationToken())
        {
            employees.Add(document);
        }

        /// <summary>
        /// Inserts a single document.
        /// </summary>
        /// <param name="document">The document.</param><param name="_cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The result of the insert operation.
        /// </returns>
        public Task InsertOneAsync(Store.MongoDb.Employee document, CancellationToken _cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Inserts a single document.
        /// </summary>
        /// <param name="document">The document.</param><param name="options">The options.</param><param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The result of the insert operation.
        /// </returns>
        public Task InsertOneAsync(Store.MongoDb.Employee document, InsertOneOptions options = null, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Inserts many documents.
        /// </summary>
        /// <param name="documents">The documents.</param><param name="options">The options.</param><param name="cancellationToken">The cancellation token.</param>
        public void InsertMany(IEnumerable<Store.MongoDb.Employee> documents, InsertManyOptions options = null, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Inserts many documents.
        /// </summary>
        /// <param name="documents">The documents.</param><param name="options">The options.</param><param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The result of the insert operation.
        /// </returns>
        public Task InsertManyAsync(IEnumerable<Store.MongoDb.Employee> documents, InsertManyOptions options = null, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Executes a map-reduce command.
        /// </summary>
        /// <param name="map">The map function.</param><param name="reduce">The reduce function.</param><param name="options">The options.</param><param name="cancellationToken">The cancellation token.</param><typeparam name="TResult">The type of the result.</typeparam>
        /// <returns>
        /// A cursor.
        /// </returns>
        public IAsyncCursor<TResult> MapReduce<TResult>(BsonJavaScript map, BsonJavaScript reduce, MapReduceOptions<Store.MongoDb.Employee, TResult> options = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Executes a map-reduce command.
        /// </summary>
        /// <param name="map">The map function.</param><param name="reduce">The reduce function.</param><param name="options">The options.</param><param name="cancellationToken">The cancellation token.</param><typeparam name="TResult">The type of the result.</typeparam>
        /// <returns>
        /// A Task whose result is a cursor.
        /// </returns>
        public Task<IAsyncCursor<TResult>> MapReduceAsync<TResult>(BsonJavaScript map, BsonJavaScript reduce, MapReduceOptions<Store.MongoDb.Employee, TResult> options = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Returns a filtered collection that appears to contain only documents of the derived type.
        ///             All operations using this filtered collection will automatically use discriminators as necessary.
        /// </summary>
        /// <typeparam name="TDerivedDocument">The type of the derived document.</typeparam>
        /// <returns>
        /// A filtered collection.
        /// </returns>
        public IFilteredMongoCollection<TDerivedDocument> OfType<TDerivedDocument>() where TDerivedDocument : Store.MongoDb.Employee
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Replaces a single document.
        /// </summary>
        /// <param name="filter">The filter.</param><param name="replacement">The replacement.</param><param name="options">The options.</param><param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The result of the replacement.
        /// </returns>
        public ReplaceOneResult ReplaceOne(FilterDefinition<Store.MongoDb.Employee> filter, Store.MongoDb.Employee replacement, UpdateOptions options = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Replaces a single document.
        /// </summary>
        /// <param name="filter">The filter.</param><param name="replacement">The replacement.</param><param name="options">The options.</param><param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The result of the replacement.
        /// </returns>
        public Task<ReplaceOneResult> ReplaceOneAsync(FilterDefinition<Store.MongoDb.Employee> filter, Store.MongoDb.Employee replacement, UpdateOptions options = null, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Updates many documents.
        /// </summary>
        /// <param name="filter">The filter.</param><param name="update">The update.</param><param name="options">The options.</param><param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The result of the update operation.
        /// </returns>
        public UpdateResult UpdateMany(FilterDefinition<Store.MongoDb.Employee> filter, UpdateDefinition<Store.MongoDb.Employee> update, UpdateOptions options = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Updates many documents.
        /// </summary>
        /// <param name="filter">The filter.</param><param name="update">The update.</param><param name="options">The options.</param><param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The result of the update operation.
        /// </returns>
        public Task<UpdateResult> UpdateManyAsync(FilterDefinition<Store.MongoDb.Employee> filter, UpdateDefinition<Store.MongoDb.Employee> update, UpdateOptions options = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Updates a single document.
        /// </summary>
        /// <param name="filter">The filter.</param><param name="update">The update.</param><param name="options">The options.</param><param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The result of the update operation.
        /// </returns>
        public UpdateResult UpdateOne(FilterDefinition<Store.MongoDb.Employee> filter, UpdateDefinition<Store.MongoDb.Employee> update, UpdateOptions options = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Updates a single document.
        /// </summary>
        /// <param name="filter">The filter.</param><param name="update">The update.</param><param name="options">The options.</param><param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The result of the update operation.
        /// </returns>
        public Task<UpdateResult> UpdateOneAsync(FilterDefinition<Store.MongoDb.Employee> filter, UpdateDefinition<Store.MongoDb.Employee> update, UpdateOptions options = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Returns a new collection with a different read concern.
        /// </summary>
        /// <param name="readConcern">The read concern.</param>
        /// <returns>
        /// A new collection.
        /// </returns>
        public IMongoCollection<Store.MongoDb.Employee> WithReadConcern(ReadConcern readConcern)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Returns a new collection with a different read preference.
        /// </summary>
        /// <param name="readPreference">The read preference.</param>
        /// <returns>
        /// A new collection.
        /// </returns>
        public IMongoCollection<Store.MongoDb.Employee> WithReadPreference(ReadPreference readPreference)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Returns a new collection with a different write concern.
        /// </summary>
        /// <param name="writeConcern">The write concern.</param>
        /// <returns>
        /// A new collection.
        /// </returns>
        public IMongoCollection<Store.MongoDb.Employee> WithWriteConcern(WriteConcern writeConcern)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gets the namespace of the collection.
        /// </summary>
        public CollectionNamespace CollectionNamespace { get; }

        /// <summary>
        /// Gets the database.
        /// </summary>
        public IMongoDatabase Database { get; }

        /// <summary>
        /// Gets the document serializer.
        /// </summary>
        public IBsonSerializer<Store.MongoDb.Employee> DocumentSerializer { get; }

        /// <summary>
        /// Gets the index manager.
        /// </summary>
        public IMongoIndexManager<Store.MongoDb.Employee> Indexes { get; }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        public MongoCollectionSettings Settings { get; }
    }
}
