using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace project_ecommerce_admin.Models
{
    public class Property
    {
        [Key]
        public Guid Id { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string Value { get; set; }

        // Foreign key table Product
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
