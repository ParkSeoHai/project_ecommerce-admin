using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_ecommerce_admin.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        [Column(TypeName = "nvarchar(15)")]
        public string PhoneNumber { get; set; }
        [Column(TypeName = "nvarchar(500)")]
        public string Address { get; set; }
        [Column(TypeName = "nvarchar(1000)")]
        public string Note { get; set; }
        public DateTime OrderDate { get; set; }

        // Foreign key table Status
        public Guid StatusId { get; set; }
        public Status Status { get; set; }

        // Foreign key table Customer
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
