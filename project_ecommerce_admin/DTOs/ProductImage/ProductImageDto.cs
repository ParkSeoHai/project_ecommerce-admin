using System.ComponentModel.DataAnnotations.Schema;

namespace project_ecommerce_admin.DTOs.ProductImage
{
    public class ProductImageDto
    {
        public Guid Id { get; set; }
        public string Src { get; set; }
        public Guid ProductId { get; set; }
    }
}
