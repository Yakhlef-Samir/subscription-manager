using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace SubscriptionManager.Infrastructure;

public class MongoSettings
{
    [Required] public string ConnectionString { get; set; } = null!;

    [Required] public string DatabaseName { get; set; } = null!;

    [Required] public string UsersCollectionName { get; set; } = null!;
}