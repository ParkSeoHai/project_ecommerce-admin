using project_ecommerce_admin.Models;

namespace project_ecommerce_admin.Repositories.Interface
{
    public interface IProductProperty
    {
        Task<List<Property>> GetPropertiesByProductIdAsync(Guid productId);
        Task<bool> AddPropertyAsync(Property property);
        Task<bool> UpdatePropertyAsync(Property property);
        Task<int> RemovePropertyByProductIdAsync(Guid productId);
    }
}
