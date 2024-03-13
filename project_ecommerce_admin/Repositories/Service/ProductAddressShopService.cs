using Microsoft.EntityFrameworkCore;
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

        public async Task<List<ProductShop>> GetAllProductShopByOptionIdAsync(Guid optionId)
        {
            var productShops = await _dbContext.ProductShops
                        .Where(p => p.OptionId == optionId)
                        .Select(p => new ProductShop
                        {
                            OptionId = p.OptionId,
                            AddressShopId = p.AddressShopId,
                            Option = p.Option,
                            AddressShop = p.AddressShop,
                            Quantity = p.Quantity
                        }).ToListAsync();
            return productShops;
        }

        public async Task<bool> UpdateProductAddressShopAsync(ProductShop productShop)
        {
            try
            {
                _dbContext.ProductShops.Update(productShop);
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
