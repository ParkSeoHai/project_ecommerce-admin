using project_ecommerce_admin.Models;

namespace project_ecommerce_admin.Repositories.Interface
{
    public interface IProductImage
    {
        Task<List<Image>> GetAllImagesByProductIdAsync(Guid productId);
        Task<Image> GetImageByIdAsync(Guid imageId);
        Task<bool> AddProductImageToDbAsync(Image productImage);
        Task<bool> UpdateProductImageAsync(Image productImage);
        Task<int> RemoveImageByProductIdAsync(Guid productId);
    }
}
