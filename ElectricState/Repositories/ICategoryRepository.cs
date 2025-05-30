using ElectricState.Models;

namespace ElectricState.Repositories
{
    public interface ICategoryRepository
    {

        Task<Category> CreateAsync(Category category);
        Task<Category> UpdateAsync(Category category);
        Task<IEnumerable<Category>> GetAllAsync();

        Task DeleteAsync(Category category);

        Task<Category> GetById(int id);

        Task<bool> ExistByNameAsync(string name);


    }
}
