using Microsoft.Extensions.Options;
using MongoDB.Driver;
using subscription_Domain.Entities;
using subscription_Domain.Repositories;
using SubscriptionManager.Infrastructure.Data;

namespace SubscriptionManager.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _userCollection;
    private readonly FilterDefinitionBuilder<User> _filterBuilder = Builders<User>.Filter;

    public UserRepository(MongoDbContext context)
    {
        _userCollection = context.Users;
    }

    public UserRepository(IMongoCollection<User> users)
    {
        _userCollection = users;
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        var filter = _filterBuilder.Eq(u => u.Id, id);
        return await _userCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        var filter = _filterBuilder.Eq(e => e.Email, email);
        return await _userCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _userCollection.Find(_filterBuilder.Empty).ToListAsync();
    }

    public async Task AddAsync(User user)
    {
        await _userCollection.InsertOneAsync(user);
        user.CreatedAt = DateTime.UtcNow;
        user.UpdatedAt = DateTime.UtcNow;
    }

    public async Task UpdateAsync(User user)
    {
        var filter = _filterBuilder.Eq(u => u.Id, user.Id);
        user.UpdatedAt = DateTime.UtcNow;
        await _userCollection.ReplaceOneAsync(filter, user);
    }

    public async Task DeleteAsync(Guid id)
    {
        var filter = _filterBuilder.Eq(u => u.Id, id);
        await _userCollection.DeleteOneAsync(filter);
    }
}