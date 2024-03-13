using project_ecommerce_admin.Models;

namespace project_ecommerce_admin.DTOs.Product
{
    public class ProductPostDto
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public Guid BrandId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Discount { get; set; }
        public int Quantity { get; set; }
        public bool Publish { get; set; }
        public string DefaultImage { get; set; }
        public List<Image> Images { get; set; }
        public List<Color> Colors { get; set; }
        public List<Option> Options { get; set; }
        public List<ProductShop> ProductShops { get; set; }
        public List<Property> Properties { get; set; }
    }
}
