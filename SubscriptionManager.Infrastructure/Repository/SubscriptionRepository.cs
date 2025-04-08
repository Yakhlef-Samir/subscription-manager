using MongoDB.Driver;
using subscription_Domain.Entities;
using subscription_Domain.Enums;
using subscription_Domain.Repositories;
using SubscriptionManager.Infrastructure.Data;

namespace SubscriptionManager.Infrastructure.Repository;

public class SubscriptionRepository : BaseRepository<Subscription>, ISubscriptionRepository
{
    private readonly IMongoCollection<Subscription> _subscriptionCollection;
    private readonly FilterDefinitionBuilder<Subscription> _filterBuilder = Builders<Subscription>.Filter;

    public SubscriptionRepository(MongoDbContext context) : base(context, nameof(Subscription))
    {
        _subscriptionCollection = context.Database.GetCollection<Subscription>(nameof(Subscription));
    }

    public async Task<IEnumerable<Subscription>> GetAllAsync()
    {
        return await _subscriptionCollection.Find(_filterBuilder.Empty).ToListAsync();
    }

    public async Task<IEnumerable<Subscription>> GetAllByUserIdAsync(Guid userId)
    {
        var filter = _filterBuilder.Eq(s => s.UserId, userId);
        return await _subscriptionCollection.Find(filter).ToListAsync();
    }

    public async Task<Subscription> GetByIdAsync(Guid id)
    {
        var filter = _filterBuilder.Eq(s => s.Id, id);
        return await _subscriptionCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Subscription>> GetByCategoryIdAsync(Guid categoryId)
    {
        var filter = _filterBuilder.Eq(s => s.CategoryId, categoryId);
        return await _subscriptionCollection.Find(filter).ToListAsync();
    }

    public async Task AddAsync(Subscription subscription)
    {
        await _subscriptionCollection.InsertOneAsync(subscription);
        subscription.CreatedAt = DateTime.UtcNow;
        subscription.UpdatedAt = DateTime.UtcNow;
    }

    public async Task UpdateAsync(Subscription subscription)
    {
        var filter = _filterBuilder.Eq(s => s.Id, subscription.Id);
        subscription.UpdatedAt = DateTime.UtcNow;
        await _subscriptionCollection.ReplaceOneAsync(filter, subscription);
    }

    public async Task<DeleteResult> DeleteAsync(Guid id)
    {
        var filter = _filterBuilder.Eq(s => s.Id, id);
        return await _subscriptionCollection.DeleteOneAsync(filter);
    }

    public async Task<IEnumerable<Subscription>> GetActiveSubscriptionsAsync()
    {
        var filter = _filterBuilder.Eq(s => s.Status, SubscriptionStatus.Active);
        return await _subscriptionCollection.Find(filter).ToListAsync();
    }

    public async Task<IEnumerable<Subscription>> GetSubscriptionsByStatusAsync(SubscriptionStatus status)
    {
        var filter = _filterBuilder.Eq(s => s.Status, status);
        return await _subscriptionCollection.Find(filter).ToListAsync();
    }

    public async Task<IEnumerable<Subscription>> GetExpiringSoonAsync(int days)
    {
        var now = DateTime.UtcNow;
        var expirationDate = now.AddDays(days);
        var filter = _filterBuilder.Lte(s => s.NextBillingDate, expirationDate);
        return await _subscriptionCollection.Find(filter).ToListAsync();
    }

    public async Task<decimal> GetTotalMonthlyExpenseAsync(Guid userId)
    {
        var filter = _filterBuilder.Eq(s => s.UserId, userId) & _filterBuilder.Eq(s => s.Status, SubscriptionStatus.Active);
        var subscriptions = await _subscriptionCollection.Find(filter).ToListAsync();
        return subscriptions.Sum(s => s.Price);
    }
}