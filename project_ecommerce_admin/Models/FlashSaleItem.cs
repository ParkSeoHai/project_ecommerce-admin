using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_ecommerce_admin.Models
{
    [PrimaryKey(nameof(FlashSaleId), nameof(ProductId))]
    public class FlashSaleItem
    {
        // Foreign key
        [Column(Order = 0)]
        public Guid FlashSaleId { get; set; }
        public FlashSale FlashSale { get; set; }
        [Column(Order = 1)]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public int Discount { get; set; }
        public int QuantitySale { get; set;}
        public int QuantitySold { get; set; }
    }
}
