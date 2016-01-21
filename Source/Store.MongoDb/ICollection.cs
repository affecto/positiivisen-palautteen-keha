using MongoDB.Driver;

namespace Affecto.PositiveFeedback.Store.MongoDb
{
    public interface ICollection<T>
    {
        IMongoCollection<T> Load();
    }
}