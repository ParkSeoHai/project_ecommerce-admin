using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_ecommerce_admin.Models
{
    public class Status
    {
        [Key]
        public Guid Id { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string Type { get; set; }
    }
}
