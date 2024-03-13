using project_ecommerce_admin.Models;

namespace project_ecommerce_admin.Repositories.Interface
{
    public interface IProductOption
    {
        Task<List<Option>> GetAllProductOptionByColorIdAsync(Guid colorId);
        Task<bool> AddProductOptionToDbAsync(Option productOption);
        Task<bool> UpdateProductOptionAsync(Option productOption);
    }
}
