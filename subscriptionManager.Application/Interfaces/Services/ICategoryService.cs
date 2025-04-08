﻿using subscription_Application.DTOs.Category;

namespace subscription_Application.Interfaces.Services;

public interface ICategoryService
{
     Task<IEnumerable<CategoryDto>>  GetCategoryAllAsync();
     Task<CategoryDto?> GetCategoryByIdAsync(Guid id); 
     Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto dto);
     Task<CategoryDto> UpdateCategoryAsync(Guid id, UpdateCategoryDto dto);
     Task<bool> DeleteCategoryAsync(Guid id);
}