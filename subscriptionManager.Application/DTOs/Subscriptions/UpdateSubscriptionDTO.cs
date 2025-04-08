using System.ComponentModel.DataAnnotations;
using subscription_Domain.Enums;

namespace subscription_Application.DTOs.Subscriptions;

public class UpdateSubscriptionDto
{
    [StringLength(100)] public string SubscriptionName { get; set; } = string.Empty;

    [StringLength(500)] public string Description { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue)] public decimal? Price { get; set; } = null;

    [StringLength(3)] public string Currency { get; set; } = string.Empty;

    public SubscriptionStatus? Status { get; set; }

    public DateTime? StartDate { get; set; }

    public string BillingCycle { get; set; }  = string.Empty;

    public string Provider { get; set; } = string.Empty;

    public Guid? CategoryId { get; set; }
}