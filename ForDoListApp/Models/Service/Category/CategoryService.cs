using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Model;
using ForDoListApp.Data;

namespace Models.Service.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(AppDbContext context, ILogger<CategoryService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<CategoryEntity> GetAllCategoriesByUserId(int userId)
        {
            if (userId < 1)
            {
                _logger.LogError("GetAllCategoriesByUserId - invalid userId.");
                return new List<CategoryEntity>();
            }

            var categories = _context.Categories
                                    .Where(c => c.UserId == userId)
                                    .ToList();

            _logger.LogInformation($"Fetched {categories.Count} categories for user {userId}.");
            return categories;
        }

        public CategoryEntity? GetCategoryById(int categoryId)
        {
            if (categoryId < 1)
            {
                _logger.LogError("GetCategoryById - invalid categoryId.");
                return null;
            }

            var category = _context.Categories.Find(categoryId);
            if (category == null)
            {
                _logger.LogWarning($"Category with id {categoryId} not found.");
            }
            else
            {
                _logger.LogInformation($"Category with id {categoryId} fetched.");
            }

            return category;
        }

        public void SaveCategory(CategoryEntity category)
        {
            if (category == null)
            {
                _logger.LogError("SaveCategory - category is null.");
                return;
            }

            _context.Categories.Add(category);
            _context.SaveChanges();

            _logger.LogInformation($"Category '{category.CategoryName}' saved.");
        }

        public void UpdateCategory(CategoryEntity category)
        {
            if (category == null || category.CategoryId < 1)
            {
                _logger.LogError("UpdateCategory - invalid category.");
                return;
            }

            _context.Categories.Update(category);
            _context.SaveChanges();

            _logger.LogInformation($"Category with id {category.CategoryId} updated.");
        }

        public void DeleteCategoryById(int categoryId)
        {
            if (categoryId < 1)
            {
                _logger.LogError("DeleteCategoryById - invalid categoryId.");
                return;
            }

            var category = _context.Categories.Find(categoryId);
            if (category == null)
            {
                _logger.LogWarning($"DeleteCategoryById - category {categoryId} not found.");
                return;
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();

            _logger.LogInformation($"Category with id {categoryId} deleted.");
        }
    }
}