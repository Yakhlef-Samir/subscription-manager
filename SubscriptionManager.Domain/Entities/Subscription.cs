using subscription_Domain.Common;
using subscription_Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace subscription_Domain.Entities;

public class Subscription : BaseEntities
{
    [Required]
    [StringLength(100)]
    public string SubscriptionName { get; set; } = null!;

    [StringLength(500)]
    public string Description { get; set; } = null!;

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Le prix doit être supérieur à zéro.")]
    public decimal Price { get; set; }

    [Required]
    [StringLength(3, ErrorMessage = "La devise doit être un code de 3 lettres.")]
    public string Currency { get; set; } = null!;

    [Required]
    public SubscriptionStatus Status { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime NextBillingDate { get; set; }

    [Required]
    public string BillingCycle { get; set; } = null!;

    [Required]
    public string Provider { get; set; } = null!;

    public Guid UserId { get; set; }
    public Guid CategoryId { get; set; }
}