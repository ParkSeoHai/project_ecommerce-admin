using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_ecommerce_admin.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        [Column(TypeName = "nvarchar(500)")]
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Discount { get; set; }
        public int Quantity { get; set; }
        public bool Publish { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set;}
        public DateTime CreatedBy { get; set; }
        public DateTime UpdatedBy { get; set;}

        // Foreign key table Category
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        // Foreign key table Brand
        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}