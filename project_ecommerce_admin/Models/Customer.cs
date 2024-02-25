using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_ecommerce_admin.Models
{
    public class Customer
    {
        [Key]
        public Guid Id { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string LastName { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string Email { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public bool EmailConfirmed { get; set; }
        [Column(TypeName = "nvarchar(1000)")]
        public string PasswordHash { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string Picture { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
