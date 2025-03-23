using MongoDB.Driver;
using SubscriptionManager.Infrastructure.Data;

namespace SubscriptionManager.Infrastructure.Repository;

// Infrastructure/Repository/BaseRepository.cs
public abstract class BaseRepository<T> where T : class
        {
            protected readonly IMongoCollection<T> Collection;
        
            protected BaseRepository(MongoDbContext context, string collectionName)
            {
                ArgumentNullException.ThrowIfNull(context);
                ArgumentException.ThrowIfNullOrEmpty(collectionName);
        
                Collection = context.Database.GetCollection<T>(collectionName);
            }
        }