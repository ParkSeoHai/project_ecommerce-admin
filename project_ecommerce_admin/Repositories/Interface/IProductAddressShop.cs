using project_ecommerce_admin.Models;

namespace project_ecommerce_admin.Repositories.Interface
{
    public interface IProductAddressShop
    {
        Task<List<ProductShop>> GetAllProductShopByOptionIdAsync(Guid optionId);
        Task<bool> AddProductAddressShopToDbAsync(ProductShop productShop);
        Task<bool> UpdateProductAddressShopAsync(ProductShop productShop);
    }
}
