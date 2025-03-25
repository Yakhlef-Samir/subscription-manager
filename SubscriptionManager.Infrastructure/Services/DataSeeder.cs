using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using subscription_Domain.Entities;
using SubscriptionManager.Infrastructure.Data;

namespace SubscriptionManager.Infrastructure.Services;

public class DataSeeder
{
    private readonly IMongoCollection<User> _userCollection;
    private readonly IPasswordHasher _passwordHasher;

    public DataSeeder(MongoDbContext database, IOptions<MongoSettings> mongoSettings, IPasswordHasher passwordHasher)
    {
        _userCollection = database.Database.GetCollection<User>(mongoSettings.Value.UsersCollectionName);
        _passwordHasher = passwordHasher;
    }

    public async Task SeederUsers()
    {
        var users = new[]
        {
            new User
            {
                Email = "test@test.com",
                UserName = "admin",
                PasswordHash = _passwordHasher.HashPassword("admin"),
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Roles = ["Admin", "User"]
            },
            new User
            {
                Email = "user@example.com",
                UserName = "john.doe",
                DisplayName = "John Doe",
                PasswordHash = _passwordHasher.HashPassword("User123!"),
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Roles = ["User"]
            }
        };

        await _userCollection.InsertManyAsync(users);
    }
}