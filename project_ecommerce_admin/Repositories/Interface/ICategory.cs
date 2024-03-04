using project_ecommerce_admin.Models;
using System.Collections;

namespace project_ecommerce_admin.Repositories.Interface
{
    public interface ICategory
    {
        Task<List<Category>> GetCategoryByLevelAsync(int level);
        Task<List<Category>> GetCategoryByCategoryIdAsync(string categoryId);
    }
}
