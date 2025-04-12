using MongoDB.Driver;
using subscription_Domain.Entities;
using subscription_Domain.Repositories;
using SubscriptionManager.Infrastructure.Data;

namespace SubscriptionManager.Infrastructure.Repository;

public class CategoryRepository :  ICategoryRepository
{
    private readonly IMongoCollection<Category> _categoryCollection;
    private readonly FilterDefinitionBuilder<Category> _categoryFilter = Builders<Category>.Filter;
    public CategoryRepository(IMongoCollection<Category> category) 
    {
        _categoryCollection = category;
    }

    public async Task<List<Category>> GetCategoryAllAsync()
    {
        return await _categoryCollection.Find(_categoryFilter.Empty).ToListAsync();
    }

    public async Task<Category?> GetCategoryByIdAsync(Guid id)
    {
        var filter = _categoryFilter.Eq(c => c.Id, id);
        return await _categoryCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<bool> CategoryExistsAsync(Guid id)
    {
        var filter = _categoryFilter.Eq(c => c.Id, id);
        return await _categoryCollection.Find(filter).AnyAsync();
    }

    public async Task<Category> CreateCategoryAsync(Category category)
    {
        await _categoryCollection.InsertOneAsync(category);
        return category;
    }

    public async Task<Category> UpdateCategoryAsync(Category category)
    {
        await _categoryCollection.ReplaceOneAsync(c => c.Id == category.Id, category);
        return category;
    }

    public async Task<DeleteResult> DeleteCategoryAsync(Guid id)
    {
        var filter = _categoryFilter.Eq(c => c.Id, id);
        return await _categoryCollection.DeleteOneAsync(filter);
    }
}