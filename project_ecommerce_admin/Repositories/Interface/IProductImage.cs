using project_ecommerce_admin.Models;

namespace project_ecommerce_admin.Repositories.Interface
{
    public interface IProductImage
    {
        Task<bool> AddProductImageToDbAsync(Image productImage);
    }
}
