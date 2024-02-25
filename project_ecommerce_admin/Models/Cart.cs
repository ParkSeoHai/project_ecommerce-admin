using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_ecommerce_admin.Models
{
    public class Cart
    {
        [Key]
        public Guid Id { get; set; }
        // Foreign key table Customer
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
