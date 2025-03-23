using System.ComponentModel.DataAnnotations;
using subscription_Domain.Common;

namespace subscription_Domain.Entities;

public class User : BaseEntities
{
    [Required] [EmailAddress] public string Email { get; set; } = null!;

    [Required] [MaxLength(50)] public string UserName { get; set; } = null!;

    [MaxLength(100)] public string DisplayName { get; set; } = null!;

    [Required] public string PasswordHash { get; set; } = null!;

    public bool IsActive { get; set; }

    //** Navigation properties
    public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    //** Constructeur paramétré
    public User(string email, string userName, string displayName, string passwordHash, bool isActive)
    {
        Email = email;
        UserName = userName;
        DisplayName = displayName;
        PasswordHash = passwordHash;
        IsActive = true;
    }

    //** Constructeur par défaut
    public User() { }
}