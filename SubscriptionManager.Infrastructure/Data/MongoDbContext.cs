using MongoDB.Driver;
using Microsoft.Extensions.Options;
using subscription_Domain.Entities;

namespace SubscriptionManager.Infrastructure.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IOptions<MongoSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        _database = client.GetDatabase(settings.Value.DatabaseName);
    }

    public IMongoDatabase Database => _database;
    public IMongoCollection<User> Users => _database.GetCollection<User>(nameof(User));
}