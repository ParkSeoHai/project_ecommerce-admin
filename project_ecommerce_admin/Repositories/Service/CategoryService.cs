using Microsoft.EntityFrameworkCore;
using project_ecommerce_admin.Data;
using project_ecommerce_admin.Models;
using project_ecommerce_admin.Repositories.Interface;

namespace project_ecommerce_admin.Repositories.Service
{
    public class CategoryService : ICategory
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<List<Category>> GetCategoryByLevelAsync(int level)
        {
            var categories = await _dbContext.Categories.Where(c => c.Level == level).ToListAsync();
            return categories;
        }

        public async Task<List<Category>> GetCategoryByCategoryIdAsync(string categoryId)
        {
            var categories = await _dbContext.Categories.Where(c => c.CategoryId == categoryId).ToListAsync();
            return categories;
        }
    }
}
