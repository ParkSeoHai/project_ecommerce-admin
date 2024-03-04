using Microsoft.EntityFrameworkCore;
using project_ecommerce_admin.Data;
using project_ecommerce_admin.Models;
using project_ecommerce_admin.Repositories.Interface;

namespace project_ecommerce_admin.Repositories.Service
{
    public class AddressShopService : IAddressShop
    {
        private readonly ApplicationDbContext _dbContext;

        public AddressShopService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<List<AddressShop>> GetAllDataAsync()
        {
            var addressShops = await _dbContext.AddressShops.ToListAsync();
            return addressShops;
        }
    }
}
