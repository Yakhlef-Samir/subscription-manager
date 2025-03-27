using MongoDB.Driver;
using subscription_Domain.Entities;
using SubscriptionManager.Infrastructure.Data;
using SubscriptionManager.Infrastructure.Repository;
using SubscriptionManager.IntegrationTests.Fixtures;

namespace SubscriptionManager.IntegrationTests.Repository;

public class UserRepositoryTests : IClassFixture<MongoDbFixture>, IDisposable
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

    [Fact]
    public async Task Should_Create_User()
    {
        // Arrange
        var user = new User { Email = "pesso@pesso.com" };

        // Act
        await _userRepository.AddAsync(user);

        // Assert
        var result = await _userRepository.GetByIdAsync(user.Id);
        Assert.NotNull(result);
        Assert.Equal(user.Email, result.Email);
    }
    void IDisposable.Dispose()
    {
        // Nettoyage de la collection après chaque test
        //_collection.DeleteMany(Builders<User>.Filter.Empty);
    }
}