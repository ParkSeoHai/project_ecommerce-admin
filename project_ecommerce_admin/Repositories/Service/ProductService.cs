using Microsoft.EntityFrameworkCore;
using project_ecommerce_admin.Data;
using project_ecommerce_admin.DTOs.Product;
using project_ecommerce_admin.Models;
using project_ecommerce_admin.Repositories.Interface;

namespace project_ecommerce_admin.Repositories.Service
{
    public class ProductService : IProduct
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<List<ProductDto>> GetAllProductsAsync() {
            var productDtos = new List<ProductDto>();

            var products = await _dbContext.Products.ToListAsync();
            foreach (var product in products)
            {
                // Create product Dto
                ProductDto productDto = new ProductDto()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Discount = product.Discount,
                    Quantity = product.Quantity,
                    Publish = product.Publish,
                    CreatedDate = product.CreatedDate,
                    CreatedBy = product.CreatedBy,
                    UpdatedDate = product.UpdatedDate,
                    UpdatedBy = product.UpdatedBy
                };
                // Get brand, category, image by product id
                productDto.Brand = await _dbContext.Brands.FirstAsync(b => b.Id == product.BrandId);
                productDto.Category = await _dbContext.Categories.FirstAsync(c => c.Id == product.CategoryId);
                productDto.Images = await _dbContext.Images.Where(i => i.ProductId == productDto.Id).ToListAsync();

                // Add to list product dto
                productDtos.Add(productDto);
            }

            return productDtos;
        }

        public async Task<bool> AddProductToDbAsync(Product product)
        {
            try
            {
                await _dbContext.Products.AddAsync(product);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> RemoveProductInDbAsync(Guid productId)
        {
            try
            {
                var product = await _dbContext.Products.FindAsync(productId);
                if (product != null)
                {
                    _dbContext.Products.Remove(product);
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
