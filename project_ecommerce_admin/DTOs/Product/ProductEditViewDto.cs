using project_ecommerce_admin.DTOs.Category;

namespace project_ecommerce_admin.DTOs.Product
{
    public class ProductEditViewDto
    {
        public ProductDto ProductDto { get; set; }
        public List<Models.Brand> Brands { get; set; }
        public List<Models.AddressShop> AddressShops { get; set; }
        public List<Models.Category> CategoriesLv1 { get; set; }
        public List<Models.Category> CategoriesLv2 { get; set; }
        public List<Models.Category> CategoriesLv3 { get; set; }
    }
}
