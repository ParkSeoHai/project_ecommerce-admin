using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_ecommerce_admin.Models
{
    public class Image
    {
        [Key]
        public Guid Id { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string Src { get; set; }

        // Foreign key table Product
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
