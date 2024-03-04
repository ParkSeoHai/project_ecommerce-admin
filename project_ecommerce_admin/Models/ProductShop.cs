using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_ecommerce_admin.Models
{
    [PrimaryKey(nameof(OptionId), nameof(AddressShopId))]
    public class ProductShop
    {
        // Foreign key
        [Column(Order = 0)]
        public Guid OptionId { get; set; }
        public Option Option { get; set; }
        [Column(Order = 1)]
        public Guid AddressShopId { get; set; }
        public AddressShop AddressShop { get; set; }

        public int Quantity { get; set; }
    }
}
