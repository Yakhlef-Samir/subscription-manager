using System.ComponentModel.DataAnnotations;
using subscription_Domain.Enums;

namespace subscription_Application.DTOs.Subscriptions;

public class CreateSubscriptionDto
{
    [Required] [StringLength(100)] public string SubscriptionName { get; set; } = string.Empty;

    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }

    [Required]
    [StringLength(3)]
    public string Currency { get; set; } = string.Empty;

    public SubscriptionStatus Status { get; set; } = SubscriptionStatus.Active;

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public string BillingCycle { get; set; }   =  string.Empty;

    [Required]
    public string Provider { get; set; } = string.Empty;

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid CategoryId { get; set; }
} 