using subscription_Domain.Entities;
using subscription_Domain.Enums;
using MongoDB.Driver;

namespace subscription_Domain.Repositories;

public interface ISubscriptionRepository
{
    Task<IEnumerable<Subscription>> GetAllAsync();
    Task<IEnumerable<Subscription>> GetAllByUserIdAsync(Guid userId);
    Task<Subscription> GetByIdAsync(Guid id);
    Task<IEnumerable<Subscription>> GetByCategoryIdAsync(Guid categoryId);
    Task AddAsync(Subscription subscription);
    Task UpdateAsync(Subscription subscription);
    Task<DeleteResult> DeleteAsync(Guid id);
    Task<IEnumerable<Subscription>> GetActiveSubscriptionsAsync();
    Task<IEnumerable<Subscription>> GetSubscriptionsByStatusAsync(SubscriptionStatus status);
    Task<IEnumerable<Subscription>> GetExpiringSoonAsync(int days);
    Task<decimal> GetTotalMonthlyExpenseAsync(Guid userId);
    
}