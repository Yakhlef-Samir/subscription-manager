using MongoDB.Driver;
using subscription_Domain.Entities;

namespace subscription_Domain.Repositories;

public interface ICategoryRepository
{
      Task<List<Category>> GetCategoryAllAsync();
      Task<Category?>GetCategoryByIdAsync(Guid id);
      Task<Category> CreateCategoryAsync(Category category);
      Task<Category> UpdateCategoryAsync(Category category);
      Task<DeleteResult> DeleteCategoryAsync(Guid id);
      Task<bool> CategoryExistsAsync(Guid id);
}