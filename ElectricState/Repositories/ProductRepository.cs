using ElectricState.DataContext;
using ElectricState.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectricState.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ElectricDbContext _context;
        private readonly ILogger<ProductRepository> _logger;
        public ProductRepository(ElectricDbContext context, ILogger<ProductRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Product> CreateProductAsync(Product product)
        {
            try
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                return product;

            }
            catch(Exception ex)
            {
                _logger.LogError(ex,"Error Occured while adding a product {@Product}", product);
                throw;

            }

        }

        public Task<Product> DeleteAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistByNameAsync(string productName)
        {
            try
            {
                return await _context.Products.AnyAsync(p => p.ProductName == productName);

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking if product exists by name: {ProductName}", productName);
                throw;
            }
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            try
            {
                return await _context.Products.Include(p => p.Category).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all products");
                throw;
            }
        }

        public Task<Product> GetByIdAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<Product> UpdateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
