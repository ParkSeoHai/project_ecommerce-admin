using project_ecommerce_admin.Models;

namespace project_ecommerce_admin.Repositories.Interface
{
    public interface IProductColor
    {
        Task<List<Color>> GetAllColorByProductIdAsync(Guid productId);
        Task<bool> AddProductColorToDbAsync(Color productColor);
        Task<bool> UpdateProductColorAsync(Color productColor);
        Task<bool> RemoveProductColorInDbAsync(Guid productColorId);
        Task<int> RemoveColorByProductIdAsync(Guid productId);
    }
}
