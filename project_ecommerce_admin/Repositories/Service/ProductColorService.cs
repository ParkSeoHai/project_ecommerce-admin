using project_ecommerce_admin.Data;
using project_ecommerce_admin.Models;
using project_ecommerce_admin.Repositories.Interface;

namespace project_ecommerce_admin.Repositories.Service
{
    public class ProductColorService : IProductColor
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductColorService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<bool> AddProductColorToDbAsync(Color productColor)
        {
            try
            {
                await _dbContext.Colors.AddAsync(productColor);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> RemoveProductColorInDbAsync(Guid productColorId)
        {
            try
            {
                var productColor = await _dbContext.Colors.FindAsync(productColorId);
                if (productColor != null)
                {
                    _dbContext.Colors.Remove(productColor);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
