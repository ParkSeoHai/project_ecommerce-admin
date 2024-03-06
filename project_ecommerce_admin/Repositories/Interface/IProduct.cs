using project_ecommerce_admin.DTOs.Product;
using project_ecommerce_admin.Models;

namespace project_ecommerce_admin.Repositories.Interface
{
    public interface IProduct
    {
        Task<List<ProductDto>> GetAllProductsAsync();
        Task<bool> AddProductToDbAsync(Product product);
        Task<bool> RemoveProductInDbAsync(Guid productId);
    }
}
