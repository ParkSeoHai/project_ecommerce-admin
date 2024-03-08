using Microsoft.AspNetCore.Mvc;
using project_ecommerce_admin.DTOs.Product;
using project_ecommerce_admin.Models;
using project_ecommerce_admin.Repositories.Interface;

namespace project_ecommerce_admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICategory _categoryService;
        private readonly IBrand _brandService;
        private readonly IAddressShop _addressShopService;
        private readonly IProduct _productService;
        private readonly IProductImage _productImageService;
        private readonly IProductColor _productColorService;
        private readonly IProductOption _productOptionService;
        private readonly IProductAddressShop _productAddressShopService;
        private readonly IProductProperty _productPropertyService;

        public ProductController(ICategory categoryService,
            IBrand brandService, IAddressShop addressShopService,
            IProduct productService, IProductImage productImageService,
            IProductColor productColorService, IProductOption productOptionService,
            IProductAddressShop productAddressShopService, IProductProperty productPropertyService)
        {
            this._categoryService = categoryService;
            this._brandService = brandService;
            this._addressShopService = addressShopService;
            this._productService = productService;
            this._productImageService = productImageService;
            this._productColorService = productColorService;
            this._productOptionService = productOptionService;
            this._productAddressShopService = productAddressShopService;
            this._productPropertyService = productPropertyService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await GetAllProducts();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var data = await GetDataCreateView();
            return View(data);
        }

        [HttpPost]
        public async Task<string> CreateProductPost([FromBody] ProductAddDto productAddDto)
        {
            if (productAddDto == null)
            {
                return "Product data is null or invalid.";
            }

            try
            {
                // Process the product data
                Product product = new Product()
                {
                    Id = productAddDto.Id,
                    Name = productAddDto.Name,
                    Description = productAddDto.Description,
                    Price = productAddDto.Price,
                    Discount = productAddDto.Discount,
                    Quantity = productAddDto.Quantity,
                    DefaultImage = productAddDto.DefaultImage,
                    Publish = productAddDto.Publish,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    CreatedBy = "Admin",
                    UpdatedBy = "Admin",
                    CategoryId = productAddDto.CategoryId,
                    BrandId = productAddDto.BrandId
                };

                bool isAddProduct = await AddProductToDb(product);
                if (isAddProduct)
                {
                    // Add product image
                    foreach (var image in productAddDto.Images)
                    {
                        bool isAddProductImage = await AddProductImageToDb(image);
                        if (isAddProductImage == false)
                        {
                            await RemoveProductDb(product.Id);
                            break;
                        }
                    }
                    // Add product color
                    foreach (var color in productAddDto.Colors)
                    {
                        bool isAddProductColor = await AddProductColorToDb(color);
                        if (isAddProductColor == false)
                        {
                            await RemoveProductDb(product.Id);
                            break;
                        }
                    }
                    // Add product option
                    foreach (var option in productAddDto.Options)
                    {
                        bool isAddProductOption = await AddProductOptionToDb(option);
                        if (isAddProductOption == false)
                        {
                            await RemoveProductDb(product.Id);
                            break;
                        }
                    }
                    // Add product address shop
                    foreach (var productShop in productAddDto.ProductShops)
                    {
                        bool isAddProductShop = await AddProductAddressShopToDb(productShop);
                        if (isAddProductShop == false)
                        {
                            await RemoveProductDb(product.Id);
                            break;
                        }
                    }
                    // Add product property
                    foreach (var property in productAddDto.Properties)
                    {
                        bool isAddProperty = await AddPropertyToDb(property);
                        if (isAddProperty == false)
                        {
                            await RemoveProductDb(product.Id);
                            break;
                        }
                    }
                    
                    return "Product created successfully.";
                }
                return "Create product failed.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // Get category when onchange select from create view
        [HttpPost]
        public async Task<IActionResult> GetCategoryById([FromBody] string categoryId)
        {
            var categories = await _categoryService.GetCategoryByCategoryIdAsync(categoryId);
            return Ok(categories);
        }

        // Get data view create product
        public async Task<ProductAddViewDto> GetDataCreateView()
        {
            var categoriesLevel = await _categoryService.GetCategoryByLevelAsync(1);
            var brands = await _brandService.GetAllBrandsAsync();
            var addressShops = await _addressShopService.GetAllDataAsync();

            ProductAddViewDto productAddView = new ProductAddViewDto()
            {
                Categories = categoriesLevel,
                Brands = brands,
                AddressShops = addressShops
            };

            return productAddView;
        }

        // Get all product from database
        public async Task<List<ProductDto>> GetAllProducts()
        {
            return await _productService.GetAllProductsAsync();
        }

        // Add product to database
        public async Task<bool> AddProductToDb(Product product)
        {
            return await _productService.AddProductToDbAsync(product);
        }

        // Remove product in database
        public async Task<bool> RemoveProductDb(Guid productId)
        {
            return await _productService.RemoveProductInDbAsync(productId);
        }

        // Add product image to database
        public async Task<bool> AddProductImageToDb(Image productImage)
        {
            return await _productImageService.AddProductImageToDbAsync(productImage);
        }

        // Add product color to database
        public async Task<bool> AddProductColorToDb(Color productColor)
        {
            return await _productColorService.AddProductColorToDbAsync(productColor);
        }

        // Add product color to database
        public async Task<bool> RemoveProductColorDb(Guid productColorId)
        {
            return await _productColorService.RemoveProductColorInDbAsync(productColorId);
        }

        // Add product option to database
        public async Task<bool> AddProductOptionToDb(Option productOption)
        {
            return await _productOptionService.AddProductOptionToDbAsync(productOption);
        }

        // Add product address shop to database
        public async Task<bool> AddProductAddressShopToDb(ProductShop productShop)
        {
            return await _productAddressShopService.AddProductAddressShopToDbAsync(productShop);
        }

        // Add product property to database
        public async Task<bool> AddPropertyToDb(Property property)
        {
            return await _productPropertyService.AddPropertyAsync(property);
        }
    }
}
