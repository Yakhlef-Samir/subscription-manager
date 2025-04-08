using subscription_Application.DTOs.Subscriptions;
using subscription_Domain.Enums;

namespace subscription_Application.Interfaces.Services;

public interface ISubscriptionService
{
    Task<IEnumerable<SubscriptionDto>> GetAllSubscriptionsAsync();
    Task<IEnumerable<SubscriptionDto>> GetAllByUserIdAsync(Guid userId);
    Task<SubscriptionDto> GetByIdAsync(Guid id);
    Task<IEnumerable<SubscriptionDto>> GetByCategoryAsync(Guid categoryId);
    Task<SubscriptionDto> CreateAsync(CreateSubscriptionDto dto);
    Task<SubscriptionDto> UpdateAsync(Guid id, UpdateSubscriptionDto dto);
    Task<bool> DeleteAsync(Guid id);
    Task<DateTime> CalculateNextBillingDateAsync(DateTime startDate, string billingCycle);
    Task<IEnumerable<SubscriptionDto>> GetActiveSubscriptionsAsync();
    Task<IEnumerable<SubscriptionDto>> GetByStatusAsync(SubscriptionStatus status);
    Task<IEnumerable<SubscriptionDto>> GetExpiringSoonAsync(int days);
    Task<decimal> GetTotalMonthlyExpenseAsync(Guid userId);
    Task<IEnumerable<SubscriptionDto>> GetSubscriptionsExpiringThisMonthAsync(Guid userId);
} 