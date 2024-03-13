using project_ecommerce_admin.Models;

namespace project_ecommerce_admin.DTOs.Product
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public Models.Category Category { get; set; }
        public Models.Brand Brand { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DefaultImage { get; set; }
        public double Price { get; set; }
        public int Discount { get; set; }
        public int Quantity { get; set; }
        public bool Publish { get; set; }
        public List<Image> Images { get; set; }
        public List<Color> Colors { get; set; }
        public List<Option> Options { get; set; }
        public List<ProductShop> ProductShops { get; set; }
        public List<Property> Properties { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
