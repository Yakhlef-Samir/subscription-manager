using subscription_Domain.Entities;
using subscription_Domain.Enums;

namespace subscription_Application.DTOs.Subscriptions;

public class SubscriptionDto(Subscription subscription)
{
    public Guid Id { get; set; } = subscription.Id;
    public string SubscriptionName { get; set; } = subscription.SubscriptionName;
    public string Description { get; set; } = subscription.Description;
    public decimal Price { get; set; } = subscription.Price;
    public string Currency { get; set; } = subscription.Currency;
    public SubscriptionStatus Status { get; set; } = subscription.Status;
    public DateTime StartDate { get; set; } = subscription.StartDate;
    public DateTime NextBillingDate { get; set; } = subscription.NextBillingDate;
    public string BillingCycle { get; set; } = subscription.BillingCycle;
    public string Provider { get; set; } = subscription.Provider;
    public Guid UserId { get; set; } = subscription.UserId;
    public Guid CategoryId { get; set; } = subscription.CategoryId;
    public DateTime CreatedAt { get; set; } = subscription.CreatedAt;
    public DateTime? UpdatedAt { get; set; } = subscription.UpdatedAt;
} 