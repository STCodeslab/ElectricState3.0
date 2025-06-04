using ElectricState.DataContext;
using ElectricState.Models;
using ElectricState.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElectricState.Repository.Implementations
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly ILogger<SupplierRepository> _logger;
        private readonly ElectricDbContext _context;

        public SupplierRepository(ILogger<SupplierRepository> logger, ElectricDbContext context)
        {
            _logger = logger;
            _context = context;

        }
        public async Task<Supplier> AddSupplierAsync(Supplier supplier)
        {
            try
            {
               await _context.Suppliers.AddAsync(supplier);
                await _context.SaveChangesAsync();
                return supplier;

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error adding supplier");
                throw;
            }
        }

        public Task<bool> DeleteSupplierAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Supplier>> GetAllSuppliersAsync()
        {
            try
            {
                return await _context.Suppliers.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occured while fetching Suppliers");
                throw new ApplicationException("An error occurred while retrieving suppliers.");
            }


        }

        public Task<Supplier?> GetSupplierByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Supplier> SearchSupplier(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SupplierExistsAsync(string name)
        {
          return  await _context.Suppliers.AnyAsync(s =>s.SupplierName == name);
        }

        public Task<Supplier> UpdateSupplierAsync(Supplier supplier)
        {
            throw new NotImplementedException();
        }
    }
}
