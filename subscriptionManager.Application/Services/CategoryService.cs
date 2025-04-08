using subscription_Application.Interfaces.Services;
using subscription_Application.DTOs.Category;
using subscription_Domain.Entities;
using subscription_Domain.Repositories;

namespace subscription_Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<CategoryDto>> GetCategoryAllAsync()
    {
        var category = await _categoryRepository.GetCategoryAllAsync();
        return category.Select(cat => new CategoryDto(cat));
    }

    public async Task<CategoryDto?> GetCategoryByIdAsync(Guid id)
    {
        var category = await _categoryRepository.GetCategoryByIdAsync(id);

        if (category == null)
        {
            return null;
        }
        return new CategoryDto(category);
    }

    public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto dto)
    {
        var category = new Category
        {
            Name = dto.Name,
            Description = dto.Description,
            IconName = dto.IconName
        };

        await _categoryRepository.CreateCategoryAsync(category);
        return new CategoryDto(category);
    }

    public async Task<CategoryDto> UpdateCategoryAsync(Guid id, UpdateCategoryDto dto)
    {
        var category = await _categoryRepository.GetCategoryByIdAsync(id) ?? throw new KeyNotFoundException("Category not found.");

        if (dto.Name != null) category.Name = dto.Name;
        if (dto.Description != null) category.Description = dto.Description;
        if (dto.IconName != null) category.IconName = dto.IconName;

        await _categoryRepository.UpdateCategoryAsync(category);
        return new CategoryDto(category);
    }

    public async Task<bool> DeleteCategoryAsync(Guid id)
    {
        var result = await _categoryRepository.DeleteCategoryAsync(id);
            return result.DeletedCount > 0;
    }


}