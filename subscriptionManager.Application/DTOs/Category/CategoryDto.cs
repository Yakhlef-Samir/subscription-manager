namespace subscription_Application.DTOs.Category;

public class CategoryDto(subscription_Domain.Entities.Category category)
{
        public Guid Id { get; set; } = category.Id;
        public string Name { get; set; } = category.Name;
        public string Description { get; set; } = category.Description;
        public string IconName { get; set; } = category.IconName;
        public DateTime CreatedAt { get; set; } = category.CreatedAt;
        public DateTime? UpdatedAt { get; set; } = category.UpdatedAt;
}