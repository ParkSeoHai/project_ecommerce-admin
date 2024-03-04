
namespace project_ecommerce_admin.DTOs.Product
{
    public class ProductAddViewDto
    {
        public List<Models.Category> Categories { get; set; }
        public List<Models.Brand> Brands { get; set; }
        public List<Models.AddressShop> AddressShops { get; set; }
    }
}
