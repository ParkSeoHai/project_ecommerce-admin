using project_ecommerce_admin.DTOs.Product;
using project_ecommerce_admin.Models;

namespace project_ecommerce_admin.Repositories.Interface
{
    public interface IProduct
    {
        Task<List<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(Guid productId);
        Task<bool> AddProductToDbAsync(Product product);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> RemoveProductInDbAsync(Guid productId);
    }
}
