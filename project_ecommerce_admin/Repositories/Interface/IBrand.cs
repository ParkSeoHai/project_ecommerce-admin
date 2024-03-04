using project_ecommerce_admin.Models;

namespace project_ecommerce_admin.Repositories.Interface
{
    public interface IBrand
    {
        Task<List<Brand>> GetAllBrandsAsync();
    }
}
