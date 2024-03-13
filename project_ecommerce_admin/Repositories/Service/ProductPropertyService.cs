using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Property>> GetPropertiesByProductIdAsync(Guid productId)
        {
            return await _dbContext.Properties.Where(p => p.ProductId == productId).ToListAsync();
        }

        public async Task<int> RemovePropertyByProductIdAsync(Guid productId)
        {
            try
            {
                var properties = await _dbContext.Properties.Where(p => p.ProductId == productId).ToListAsync();
                foreach (var property in properties)
                {
                    _dbContext.Properties.Remove(property);
                    await _dbContext.SaveChangesAsync();
                }
                return properties.Count();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<bool> UpdatePropertyAsync(Property property)
        {
            try
            {
                _dbContext.Properties.Update(property);
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
