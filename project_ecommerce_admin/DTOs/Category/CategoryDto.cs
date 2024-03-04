namespace project_ecommerce_admin.DTOs.Category
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? CategoryId { get; set; }
        public int Level { get; set; }
    }
}
