using ElectricState.Models;
using ElectricState.ViewModels.Supplier;

namespace ElectricState.Services.Interfaces
{
    public interface ISupplierService
    {
        Task<SupplierViewModel> AddSupplierAsync(SupplierViewModel supplier);
        Task<SupplierViewModel?> GetSupplierByIdAsync(int id);
        Task<IEnumerable<SupplierViewModel>> GetAllSuppliersAsync();
        Task<SupplierViewModel> UpdateSupplierAsync(SupplierViewModel supplier);
        Task<bool> DeleteSupplierAsync(int id);
    }

}
