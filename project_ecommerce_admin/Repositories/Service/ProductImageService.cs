using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Image>> GetAllImagesByProductIdAsync(Guid productId)
        {
            return await _dbContext.Images
                .Where(i => i.ProductId == productId)
                .Select(i => new Image
                {
                    Id = i.Id,
                    ProductId = productId,
                    Src = i.Src,
                }).ToListAsync();
        }

        public async Task<Image> GetImageByIdAsync(Guid imageId)
        {
            Image? image = await _dbContext.Images.FindAsync(imageId);
            return image;
        }

        public async Task<int> RemoveImageByProductIdAsync(Guid productId)
        {
            try
            {
                var images = await _dbContext.Images.Where(i => i.ProductId == productId).ToListAsync();

                foreach (var image in images)
                {
                    _dbContext.Images.Remove(image);
                    await _dbContext.SaveChangesAsync();
                }
                return images.Count();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<bool> UpdateProductImageAsync(Image productImage)
        {
            try
            {
                _dbContext.Images.Update(productImage);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
