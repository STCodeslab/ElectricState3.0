using ElectricState.Models;

namespace ElectricState.Repositories
{
    public interface IProductRepository
    {
        Task<Product> CreateProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product product);

        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product> GetByIdAsync(int productId);

        Task<Product> DeleteAsync(Product product);

        Task<bool> ExistByNameAsync(string productName);

    }
}
