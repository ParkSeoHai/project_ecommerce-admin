using project_ecommerce_admin.Models;

namespace project_ecommerce_admin.Repositories.Interface
{
    public interface IProductProperty
    {
        Task<bool> AddPropertyAsync(Property property);
    }
}
