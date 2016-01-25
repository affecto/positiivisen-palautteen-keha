using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace Affecto.PositiveFeedback.Store.MongoDb
{
    public interface ICollection<T>
    {
        IMongoCollection<T> Load();
        IGridFSBucket CreateGridFSBucket();
    }
}