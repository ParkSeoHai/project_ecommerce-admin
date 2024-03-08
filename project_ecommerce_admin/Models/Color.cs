using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_ecommerce_admin.Models
{
    public class Color
    {
        [Key]
        public Guid Id { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        // Foreign key table Product
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
