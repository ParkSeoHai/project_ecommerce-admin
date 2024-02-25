using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_ecommerce_admin.Models
{
    [PrimaryKey(nameof(OrderId), nameof(ProductId))]
    public class OrderItem
    {
        // Foreign key
        [Column(Order = 0)]
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        [Column(Order = 1)]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
