using Microsoft.EntityFrameworkCore;
using project_ecommerce_admin.Data;
using project_ecommerce_admin.Models;
using project_ecommerce_admin.Repositories.Interface;

namespace project_ecommerce_admin.Repositories.Service
{
    public class ProductOptionService : IProductOption
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductOptionService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<bool> AddProductOptionToDbAsync(Option productOption)
        {
			try
			{
                await _dbContext.Options.AddAsync(productOption);
                await _dbContext.SaveChangesAsync();
                return true;
			}
			catch (Exception)
			{
                return false;
			}
        }

        public async Task<List<Option>> GetAllProductOptionByColorIdAsync(Guid colorId)
        {
            var options = await _dbContext.Options
                        .Where(o => o.ColorId == colorId)
                        .Select(o => new Option
                        {
                            Id = o.Id,
                            Name = o.Name,
                            Value = o.Value,
                            Price = o.Price,
                            Quantity = o.Quantity,
                            ColorId = colorId,
                            Color = o.Color
                        }).ToListAsync();
            return options;
        }

        public async Task<bool> UpdateProductOptionAsync(Option productOption)
        {
            try
            {
                _dbContext.Options.Update(productOption);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
