using project_ecommerce_admin.Data;
using project_ecommerce_admin.Models;
using project_ecommerce_admin.Repositories.Interface;

namespace project_ecommerce_admin.Repositories.Service
{
    public class ProductImageService : IProductImage
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductImageService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<bool> AddProductImageToDbAsync(Image productImage)
        {
            try
            {
                await _dbContext.Images.AddAsync(productImage);
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
