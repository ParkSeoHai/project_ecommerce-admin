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
        private readonly IProductImage _productImageService;
        private readonly IProductColor _productColorService;
        private readonly IProductProperty _productPropertyService;
        private readonly IProductOption _productOptionService;
        private readonly IProductAddressShop _productShopService;

        public ProductService(
            ApplicationDbContext dbContext, IProductImage productImageService,
            IProductColor productColorService, IProductProperty productPropertyService,
            IProductOption productOptionService, IProductAddressShop productShopService)
        {
            this._dbContext = dbContext;
            this._productImageService = productImageService;
            this._productColorService = productColorService;
            this._productPropertyService = productPropertyService;
            this._productOptionService = productOptionService;
            this._productShopService = productShopService;
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
                    DefaultImage = product.DefaultImage,
                    // Get brand, category by product id
                    Brand = await _dbContext.Brands.FirstAsync(b => b.Id == product.BrandId),
                    Category = await _dbContext.Categories.FirstAsync(c => c.Id == product.CategoryId),
                    CreatedDate = product.CreatedDate,
                    CreatedBy = product.CreatedBy,
                    UpdatedDate = product.UpdatedDate,
                    UpdatedBy = product.UpdatedBy
                };

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

        public async Task<ProductDto> GetProductByIdAsync(Guid productId)
        {
            Product? product = await _dbContext.Products.FindAsync(productId);

            if(product != null)
            {
                ProductDto? productDto = new ProductDto()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Brand = product.Brand,
                    Category = product.Category,
                    Description = product.Description,
                    DefaultImage = product.DefaultImage,
                    Price = product.Price,
                    Discount = product.Discount,
                    Quantity = product.Quantity,
                    Publish = product.Publish,
                    Images = await _productImageService.GetAllImagesByProductIdAsync(productId),
                    Colors = await _productColorService.GetAllColorByProductIdAsync(productId),
                    Properties = await _productPropertyService.GetPropertiesByProductIdAsync(productId),
                    CreatedDate = product.CreatedDate,
                    CreatedBy = product.CreatedBy,
                    UpdatedDate = product.UpdatedDate,
                    UpdatedBy = product.UpdatedBy
                };

                // Get product option
                var options = new List<Option>();

                foreach (var color in productDto.Colors)
                {
                    var optionQuery = await _productOptionService.GetAllProductOptionByColorIdAsync(color.Id);
                    foreach (var option in optionQuery)
                    {
                        options.Add(option);
                    }
                }
                productDto.Options = options;

                var productShops = new List<ProductShop>();
                // Get product shop address
                foreach (var option in productDto.Options)
                {
                    var productShopsQuery = await _productShopService.GetAllProductShopByOptionIdAsync(option.Id);
                    foreach (var productShop in productShopsQuery)
                    {
                        productShops.Add(productShop);
                    }
                }
                productDto.ProductShops = productShops;

                return productDto;
            }

            return null;
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            try
            {
                if(product == null) return false;

                _dbContext.Products.Update(product);
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
