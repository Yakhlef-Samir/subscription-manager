using subscription_Domain.Common;

namespace subscription_Domain.Entities;

public class Category : BaseEntities
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string IconName { get; set; } = string.Empty;
}