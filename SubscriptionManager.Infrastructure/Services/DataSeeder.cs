
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using subscription_Domain.Entities;
using SubscriptionManager.Infrastructure.Data;

namespace SubscriptionManager.Infrastructure.Services;

public class DataSeeder
{
    private readonly IMongoCollection<User> _userCollection;
    private readonly IPasswordHasher<User> _passwordHasher;

    public DataSeeder(MongoDbContext database, IOptions<MongoSettings> mongoSettings, IPasswordHasher<User> passwordHasher)
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
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Roles = ["User"]
            }
        };

        // Ajout du hash de mot de passe après création des objets
        users[0].PasswordHash = _passwordHasher.HashPassword(users[0], "admin");
        users[1].PasswordHash = _passwordHasher.HashPassword(users[1], "password123");

        await _userCollection.InsertManyAsync(users);
    }
}