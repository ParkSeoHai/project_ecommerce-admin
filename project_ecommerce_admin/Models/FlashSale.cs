using System.ComponentModel.DataAnnotations;

namespace project_ecommerce_admin.Models
{
    public class FlashSale
    {
        [Key]
        public Guid Id { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }
    }
}
