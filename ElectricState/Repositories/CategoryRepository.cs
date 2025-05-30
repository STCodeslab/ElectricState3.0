using ElectricState.DataContext;
using ElectricState.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ElectricState.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ElectricDbContext _context;
        private readonly ILogger<CategoryRepository> _logger;


        public CategoryRepository(ElectricDbContext context, ILogger<CategoryRepository> logger)
        {
            _context = context;
            _logger = logger;

        }

        public async Task<bool> ExistByNameAsync(string name)
        {
            try
            {
                return await _context.Categories.AnyAsync(c => c.CategoryName == name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking if category exists by name: {CategoryName}", name);
                throw;
            }

        }
        public async Task<Category> CreateAsync(Category category)
        {
            try
            {
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();
                return category;

            }

            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating category: {@Category}", category);
                throw;
            }
           
        }

        public async Task DeleteAsync(Category category)
        {
            try
            {
                var existingCategory = await _context.Categories.FindAsync(category.CategoryId);
                if(existingCategory != null)
                {
                    _context.Categories.Remove(existingCategory);
                    await _context.SaveChangesAsync();
                }
            }

            catch(Exception ex)
            {
                 _logger.LogError(ex, "Error occurred while deleting category: {@Category}", category);
                throw;
            }
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            try
            {
                return await _context.Categories.ToListAsync();

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error Fetching Categories");
                return Enumerable.Empty<Category>();
            }
        }

        public Task<Category> GetById(int id)
        {
            try
            {
                return _context.Categories.FirstOrDefaultAsync(c=>c.CategoryId == id);
            }
            catch(Exception ex)
            {
                _logger.LogError("Error fetching Category id with {Id}", id);
                throw;
            }
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            try
            {
                var existingCategory = await _context.Categories.FindAsync(category.CategoryId);
                if(existingCategory == null)
                {
                    _logger.LogError("Category not found with id {id}", category.CategoryId);
                    return null;

                }

                existingCategory.CategoryName = category.CategoryName;
                existingCategory.UpdatedAt = DateTime.UtcNow;
                _context.Categories.Update(existingCategory);
             await _context.SaveChangesAsync();
                return existingCategory;
            }

            catch(Exception ex)
            {
                _logger.LogError("Error occured while updating a category {category}", category);
                throw;
            }
        }
    }
}
