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
    private bool _disposed = false;

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
            CreatedAt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, DateTimeKind.Utc),
            UpdatedAt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, DateTimeKind.Utc),
        };

        // Act
        await _userRepository.AddAsync(user);

        // Assert
        var result = await _userRepository.GetByIdAsync(user.Id);
        Assert.That(result, Is.Not.Null);
        Assert.Equals(user.Email, result.Email);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                // Libérer les ressources managées ici
                _collection.DeleteMany(Builders<User>.Filter.Empty);
            }

            _disposed = true;
        }
    }

    public new void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

}