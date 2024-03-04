using project_ecommerce_admin.Models;

namespace project_ecommerce_admin.Repositories.Interface
{
    public interface IProduct
    {
        Task<bool> AddProductToDbAsync(Product product);
        Task<bool> RemoveProductInDbAsync(Guid productId);
    }
}
