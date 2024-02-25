using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_ecommerce_admin.Models
{
    public class News
    {
        [Key]
        public Guid Id { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string Title { get; set; }
        [Column(TypeName = "nvarchar(500)")]
        public string Description { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string Author { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
