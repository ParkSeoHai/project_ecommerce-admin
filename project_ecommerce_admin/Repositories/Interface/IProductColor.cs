using project_ecommerce_admin.Models;

namespace project_ecommerce_admin.Repositories.Interface
{
    public interface IProductColor
    {
        Task<bool> AddProductColorToDbAsync(Color productColor);
        Task<bool> RemoveProductColorInDbAsync(Guid productColorId);
    }
}
