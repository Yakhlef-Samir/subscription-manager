using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using subscription_Domain.Entities;
using subscription_Domain.Repositories;
using subscription_Application.DTOs.Subscriptions;
using subscription_Application.Interfaces.Services;
using subscription_Domain.Enums;

namespace subscription_Application.Services;

public class SubscriptionService : ISubscriptionService
{
    private readonly ISubscriptionRepository _subscriptionRepository;

    public SubscriptionService(ISubscriptionRepository subscriptionRepository)
    {
        _subscriptionRepository = subscriptionRepository;
    }

    public async Task<IEnumerable<SubscriptionDto>> GetAllSubscriptionsAsync()
    {
        var subscriptions = await _subscriptionRepository.GetAllAsync();
        return subscriptions.Select(s => new SubscriptionDto(s));
    }

    public async Task<IEnumerable<SubscriptionDto>> GetAllByUserIdAsync(Guid userId)
    {
        var subscriptions = await _subscriptionRepository.GetAllByUserIdAsync(userId);
        return subscriptions.Select(s => new SubscriptionDto(s));
    }

    public async Task<SubscriptionDto> GetByIdAsync(Guid id)
    {
        var subscription = await _subscriptionRepository.GetByIdAsync(id);
        return subscription != null ? new SubscriptionDto(subscription) : null;
    }

    public async Task<IEnumerable<SubscriptionDto>> GetByCategoryAsync(Guid categoryId)
    {
        var subscriptions = await _subscriptionRepository.GetByCategoryIdAsync(categoryId);
        return subscriptions.Select(s => new SubscriptionDto(s));
    }

    public async Task<SubscriptionDto> CreateAsync(CreateSubscriptionDto dto)
    {
        var subscription = new Subscription
        {
            SubscriptionName = dto.SubscriptionName,
            Description = dto.Description,
            Price = dto.Price,
            Currency = dto.Currency,
            Status = dto.Status,
            StartDate = dto.StartDate,
            BillingCycle = dto.BillingCycle,
            Provider = dto.Provider,
            UserId = dto.UserId,
            CategoryId = dto.CategoryId
        };

        await _subscriptionRepository.AddAsync(subscription);
        return new SubscriptionDto(subscription);
    }

    public async Task<SubscriptionDto> UpdateAsync(Guid id, UpdateSubscriptionDto dto)
    {
        var subscription = await _subscriptionRepository.GetByIdAsync(id);
        if (subscription == null) return null;

        if (dto.SubscriptionName != null) subscription.SubscriptionName = dto.SubscriptionName;
        if (dto.Description != null) subscription.Description = dto.Description;
        if (dto.Price.HasValue) subscription.Price = dto.Price.Value;
        if (dto.Currency != null) subscription.Currency = dto.Currency;
        if (dto.Status.HasValue) subscription.Status = dto.Status.Value;
        if (dto.StartDate.HasValue) subscription.StartDate = dto.StartDate.Value;
        if (dto.BillingCycle != null) subscription.BillingCycle = dto.BillingCycle;
        if (dto.Provider != null) subscription.Provider = dto.Provider;
        if (dto.CategoryId.HasValue) subscription.CategoryId = dto.CategoryId.Value;

        await _subscriptionRepository.UpdateAsync(subscription);
        return new SubscriptionDto(subscription);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var result = await _subscriptionRepository.DeleteAsync(id);
        return result.DeletedCount > 0;
    }

    public Task<DateTime> CalculateNextBillingDateAsync(DateTime startDate, string billingCycle)
    {
        // Implémentation de la logique de calcul de la prochaine date de facturation
        // Cette méthode est un exemple et doit être ajustée selon vos besoins
        var nextBillingDate = startDate;
        switch (billingCycle.ToLower())
        {
            case "monthly":
                nextBillingDate = nextBillingDate.AddMonths(1);
                break;
            case "yearly":
                nextBillingDate = nextBillingDate.AddYears(1);
                break;
            // Ajoutez d'autres cycles de facturation ici
            default:
                throw new ArgumentException("Billing cycle not supported", nameof(billingCycle));
        }
        return Task.FromResult(nextBillingDate);
    }

    public async Task<IEnumerable<SubscriptionDto>> GetActiveSubscriptionsAsync()
    {
        var subscriptions = await _subscriptionRepository.GetActiveSubscriptionsAsync();
        return subscriptions.Select(s => new SubscriptionDto(s));
    }

    public async Task<IEnumerable<SubscriptionDto>> GetByStatusAsync(SubscriptionStatus status)
    {
        var subscriptions = await _subscriptionRepository.GetSubscriptionsByStatusAsync(status);
        return subscriptions.Select(s => new SubscriptionDto(s));
    }

    public async Task<IEnumerable<SubscriptionDto>> GetExpiringSoonAsync(int days)
    {
        var subscriptions = await _subscriptionRepository.GetExpiringSoonAsync(days);
        return subscriptions.Select(s => new SubscriptionDto(s));
    }

    public async Task<decimal> GetTotalMonthlyExpenseAsync(Guid userId)
    {
        return await _subscriptionRepository.GetTotalMonthlyExpenseAsync(userId);
    }

    public async Task<IEnumerable<SubscriptionDto>> GetSubscriptionsExpiringThisMonthAsync(Guid userId)
    {
        var now = DateTime.UtcNow;
        var endOfMonth = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month), 23, 59, 59);
        var subscriptions = await _subscriptionRepository.GetExpiringSoonAsync((endOfMonth - now).Days);
        return subscriptions.Where(s => s.UserId == userId).Select(s => new SubscriptionDto(s));
    }
}