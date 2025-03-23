namespace subscription_Domain.Common;

public abstract class BaseEntities
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
