using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_ecommerce_admin.Models
{
    public class AddressShop
    {
        [Key]
        public Guid Id { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string NameShop { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string City { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string District { get; set; }
        [Column(TypeName = "nvarchar(500)")]
        public string Address { get; set; }
        [Column(TypeName = "nvarchar(15)")]
        public string PhoneNumber { get; set; }
        [Column(TypeName = "nvarchar(500)")]
        public string Note { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string UrlMap { get; set; }
    }
}
