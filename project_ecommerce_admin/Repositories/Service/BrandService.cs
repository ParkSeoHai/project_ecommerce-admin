using Microsoft.EntityFrameworkCore;
using project_ecommerce_admin.Data;
using project_ecommerce_admin.Models;
using project_ecommerce_admin.Repositories.Interface;

namespace project_ecommerce_admin.Repositories.Service
{
    public class BrandService : IBrand
    {
        private readonly ApplicationDbContext _dbContext;
        public BrandService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<List<Brand>> GetAllBrandsAsync()
        {
            var brands = await _dbContext.Brands.ToListAsync();
            return brands;
        }
    }
}
