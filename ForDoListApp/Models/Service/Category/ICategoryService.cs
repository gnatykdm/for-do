using System.Collections.Generic;
using Model;

namespace Models.Service.Category
{
    public interface ICategoryService
    {
        List<CategoryEntity> GetAllCategoriesByUserId(int userId);
        CategoryEntity? GetCategoryById(int categoryId);
        void SaveCategory(CategoryEntity category);
        void UpdateCategory(CategoryEntity category);
        void DeleteCategoryById(int categoryId);
    }
}
