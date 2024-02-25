using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_ecommerce_admin.Models
{
    [PrimaryKey(nameof(CartId), nameof(ProductId))]
    public class CartItem
    {
        // Foreign key
        [Column(Order = 0)]
        public Guid CartId { get; set; }
        public Cart Cart { get; set; }
        [Column(Order = 1)]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
