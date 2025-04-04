using MongoDB.Driver;
using NUnit.Framework;
using subscription_Domain.Entities;
using SubscriptionManager.Infrastructure.Repository;
using SubscriptionManager.IntegrationTests.Fixtures;
using SubscriptionManager.IntegrationTests.HelperExtensions;

namespace SubscriptionManager.IntegrationTests.Repository;

public class UserRepositoryTests : MongoDbFixture, IDisposable
{
    private readonly IMongoCollection<User> _collection;
    private readonly UserRepository _userRepository;
    private readonly MongoDbFixture _fixture;

    public UserRepositoryTests(MongoDbFixture fixture)
    {
        _fixture = fixture;
        _collection = _fixture.Database.GetCollection<User>("User");
        _userRepository = new UserRepository(_collection);
    }


    public async Task Should_Create_User()
    {
        await _collection.DeleteManyAsync(Builders<User>.Filter.Empty);

        var user = new User
        {
            UserName = HelperExtension.GetRandomUsername(),
            DisplayName = HelperExtension.GetRandomDisplayName(),
            Email = HelperExtension.GetRandomEmail(),
            IsActive = true,
            PasswordHash = HelperExtension.GenerateRandomPassword(),
            Roles = [HelperExtension.GetRandomRoles()],
            Subscriptions = new List<Subscription>(),
            Notifications = new List<Notification>(),
            CreatedAt = new DateTime().Date,
            UpdatedAt = new DateTime().Date,
        };

        // Act
        await _userRepository.AddAsync(user);

        // Assert
        var result = await _userRepository.GetByIdAsync(user.Id);
        Assert.That(result, Is.Not.Null);
        Assert.Equals(user.Email, result.Email);
    }

    void IDisposable.Dispose()
    {
        // Nettoyage de la collection après chaque test
        //_collection.DeleteMany(Builders<User>.Filter.Empty);
    }
}