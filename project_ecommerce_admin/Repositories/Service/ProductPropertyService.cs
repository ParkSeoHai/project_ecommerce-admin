using project_ecommerce_admin.Data;
using project_ecommerce_admin.Models;
using project_ecommerce_admin.Repositories.Interface;

namespace project_ecommerce_admin.Repositories.Service
{
    public class ProductPropertyService : IProductProperty
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductPropertyService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<bool> AddPropertyAsync(Property property)
        {
			try
			{
                await _dbContext.Properties.AddAsync(property);
                await _dbContext.SaveChangesAsync();
                return true;
			}
			catch (Exception)
			{
                return false;
			}
        }
    }
}
