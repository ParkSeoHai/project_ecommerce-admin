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
            var products = await _productService.GetAllProductsAsync();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var data = await GetDataCreateView();
            return View(data);
        }

        [HttpPost]
        public async Task<string> CreateProductPost([FromBody] ProductPostDto productAddDto)
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

                bool isAddProduct = await _productService.AddProductToDbAsync(product);
                if (isAddProduct)
                {
                    // Add product image
                    foreach (var image in productAddDto.Images)
                    {
                        bool isAddProductImage = await _productImageService.AddProductImageToDbAsync(image);
                        if (isAddProductImage == false)
                        {
                            await _productService.RemoveProductInDbAsync(product.Id);
                            return "Create product image failed.";
                        }
                    }
                    // Add product color
                    foreach (var color in productAddDto.Colors)
                    {
                        bool isAddProductColor = await _productColorService.AddProductColorToDbAsync(color);
                        if (isAddProductColor == false)
                        {
                            await _productService.RemoveProductInDbAsync(product.Id);
                            return "Create product color failed.";
                        }
                    }
                    // Add product option
                    foreach (var option in productAddDto.Options)
                    {
                        bool isAddProductOption = await _productOptionService.AddProductOptionToDbAsync(option);
                        if (isAddProductOption == false)
                        {
                            await _productService.RemoveProductInDbAsync(product.Id);
                            return "Create product option failed.";
                        }
                    }
                    // Add product address shop
                    foreach (var productShop in productAddDto.ProductShops)
                    {
                        bool isAddProductShop = await _productAddressShopService.AddProductAddressShopToDbAsync(productShop);
                        if (isAddProductShop == false)
                        {
                            await _productService.RemoveProductInDbAsync(product.Id);
                            return "Create product address shop failed.";
                        }
                    }
                    // Add product property
                    foreach (var property in productAddDto.Properties)
                    {
                        bool isAddProperty = await _productPropertyService.AddPropertyAsync(property);
                        if (isAddProperty == false)
                        {
                            await _productService.RemoveProductInDbAsync(product.Id);
                            return "Create product property failed.";
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

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] Guid id)
        {
            if(id == Guid.Empty)
            {
                return BadRequest();
            }

            var data = await GetDataEditView(id);
            return View(data);
        }

        [HttpPost]
        public async Task<string> EditProductPost([FromBody] ProductPostDto productEditDto)
        {
            if (productEditDto == null)
            {
                return "Update product failed";
            }

            // Update product
            try
            {
                Product product = new Product()
                {
                    Id = productEditDto.Id,
                    Name = productEditDto.Name,
                    Description = productEditDto.Description,
                    BrandId = productEditDto.BrandId,
                    CategoryId = productEditDto.CategoryId,
                    DefaultImage = productEditDto.DefaultImage,
                    Price = productEditDto.Price,
                    Discount = productEditDto.Discount,
                    Quantity = productEditDto.Quantity,
                    Publish = productEditDto.Publish,
                    CreatedBy = "Admin",
                    UpdatedBy = "Admin",
                    UpdatedDate = DateTime.Now,
                };
                bool isUpdate = await _productService.UpdateProductAsync(product);

                if(isUpdate)
                {
                    // Remove all image
                    await _productImageService.RemoveImageByProductIdAsync(product.Id);
                    // Add product image
                    foreach (var image in productEditDto.Images)
                    {
                        Image imageObj = new Image()
                        {
                            Id = image.Id,
                            Src = image.Src,
                            ProductId = image.ProductId
                        };
                        await _productImageService.AddProductImageToDbAsync(imageObj);
                    }

                    // Remove all color
                    await _productColorService.RemoveColorByProductIdAsync(product.Id);
                    // Add product color
                    foreach (var color in productEditDto.Colors)
                    {
                        Color colorObj = new Color()
                        {
                            Id = color.Id,
                            Name = color.Name,
                            Price = color.Price,
                            Quantity = color.Quantity,
                            ProductId = color.ProductId
                        };
                        await _productColorService.AddProductColorToDbAsync(colorObj);
                    }

                    // Add product option
                    foreach (var option in productEditDto.Options)
                    {
                        Option optionObj = new Option()
                        {
                            Id = option.Id,
                            Name = option.Name,
                            Value = option.Value,
                            Quantity = option.Quantity,
                            Price = option.Price,
                            ColorId = option.ColorId
                        };
                        await _productOptionService.AddProductOptionToDbAsync(optionObj);
                    }

                    // Add product address shop
                    foreach (var productShop in productEditDto.ProductShops)
                    {
                        ProductShop productShopObj = new ProductShop()
                        {
                            OptionId = productShop.OptionId,
                            AddressShopId = productShop.AddressShopId,
                            Quantity = productShop.Quantity,
                        };
                        await _productAddressShopService.AddProductAddressShopToDbAsync(productShop);
                    }

                    // Remove all product property
                    await _productPropertyService.RemovePropertyByProductIdAsync(product.Id);
                    // Add product property
                    foreach (var property in productEditDto.Properties)
                    {
                        Property propertyObj = new Property()
                        {
                            Id = property.Id,
                            Name = property.Name,
                            Value = property.Value,
                            ProductId = product.Id,
                        };

                        await _productPropertyService.AddPropertyAsync(propertyObj);
                    }

                    return "Update product success";
                }

                return "Update product failed";
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

        // Get data view edit product
        public async Task<ProductEditViewDto> GetDataEditView(Guid productId)
        {
            // Get category list dto level 1,2,3
            var categoriesLevel1 = await _categoryService.GetCategoryByLevelAsync(1);
            var categoriesLevel2 = await _categoryService.GetCategoryByLevelAsync(2);
            var categoriesLevel3 = await _categoryService.GetCategoryByLevelAsync(3);

            var brands = await _brandService.GetAllBrandsAsync();
            var addressShops = await _addressShopService.GetAllDataAsync();

            ProductEditViewDto productEditView = new ProductEditViewDto()
            {
                ProductDto = await _productService.GetProductByIdAsync(productId),
                Brands = brands,
                AddressShops = addressShops,
                CategoriesLv1 = categoriesLevel1,
                CategoriesLv2 = categoriesLevel2,
                CategoriesLv3 = categoriesLevel3
            };

            return productEditView;
        }
    }
}
