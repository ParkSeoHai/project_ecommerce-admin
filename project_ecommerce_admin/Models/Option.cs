using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_ecommerce_admin.Models
{
    public class Option
    {
        [Key]
        public Guid Id { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string Value { get; set; }
        public double PricePlus { get; set; }
        public int Quantity { get; set; }

        // Foreign key table Color
        public Guid ColorId { get; set; }
        public Color Color { get; set; }
    }
}
