using MongoDB.Driver;
using subscription_Domain.Entities;
using subscription_Domain.Repositories;
using SubscriptionManager.Infrastructure.Data;

namespace SubscriptionManager.Infrastructure.Repository;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    private readonly IMongoCollection<Category> _categoryCollection;
    private readonly FilterDefinitionBuilder<Category> _categoryFilter = Builders<Category>.Filter;

    public CategoryRepository(MongoDbContext context) : base(context, nameof(Category))
    {
        _categoryCollection = context.Database.GetCollection<Category>(nameof(Category));
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
        var filter =  _categoryFilter.Eq(c => c.Id, id);
        return await _categoryCollection.DeleteOneAsync(filter);
    }
}