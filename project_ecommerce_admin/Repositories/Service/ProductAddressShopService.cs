using project_ecommerce_admin.Data;
using project_ecommerce_admin.Models;
using project_ecommerce_admin.Repositories.Interface;

namespace project_ecommerce_admin.Repositories.Service
{
    public class ProductAddressShopService : IProductAddressShop
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductAddressShopService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<bool> AddProductAddressShopToDbAsync(ProductShop productShop)
        {
            try
            {
                await _dbContext.ProductShops.AddAsync(productShop);
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
