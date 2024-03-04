namespace project_ecommerce_admin.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? CategoryId { get; set; }
        public int Level { get; set; }
    }
}
