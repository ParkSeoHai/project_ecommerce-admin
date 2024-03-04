using project_ecommerce_admin.Data;
using project_ecommerce_admin.Models;
using project_ecommerce_admin.Repositories.Interface;

namespace project_ecommerce_admin.Repositories.Service
{
    public class ProductOptionService : IProductOption
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductOptionService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<bool> AddProductOptionToDbAsync(Option productOption)
        {
			try
			{
                await _dbContext.Options.AddAsync(productOption);
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
