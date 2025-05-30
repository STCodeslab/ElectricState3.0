using AutoMapper;
using ElectricState.Models;
using ElectricState.Repository.Interfaces;
using ElectricState.Services.Interfaces;
using ElectricState.ViewModels.Supplier;
using Microsoft.IdentityModel.Tokens;

namespace ElectricState.Services.Implementations
{
    public class SupplierService : ISupplierService
    {
        private readonly ILogger<SupplierService> _logger;
        private readonly ISupplierRepository? _supplierRepository;
        private readonly IMapper _mapper;

        public SupplierService(ILogger<SupplierService> logger, ISupplierRepository supplierRepository, IMapper mapper)
        {
            _logger = logger;
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }
 

        public async Task<SupplierViewModel> AddSupplierAsync(SupplierViewModel vm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(vm.SupplierName))
                {
                    _logger.LogWarning("Supplier name is null or empty.");
                    throw new ArgumentNullException(nameof(vm.SupplierName), "Supplier name cannot be empty.");
                }

                if (await _supplierRepository.SupplierExistsAsync(vm.SupplierName))
                {
                    _logger.LogWarning("Supplier with name {SupplierName} already exists.", vm.SupplierName);
                    throw new InvalidOperationException($"A supplier with the name '{vm.SupplierName}' already exists.");
                }

                var supplierEntity = _mapper.Map<Supplier>(vm);
                var savedSupplier = await _supplierRepository.AddSupplierAsync(supplierEntity);
                return _mapper.Map<SupplierViewModel>(savedSupplier);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, "Validation failed while adding supplier: {@Supplier}", vm);
                throw new ApplicationException("Supplier name is required.");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Business rule violation: {Message}", ex.Message);
                throw; // Let controller handle specific duplicate error
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, "Application exception: {Message}", ex.Message);
                throw; // Friendly, handled exception
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while adding supplier: {@Supplier}", vm);
                throw new ApplicationException("An unexpected error occurred while adding the supplier.");
            }
        }


        public Task<bool> DeleteSupplierAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SupplierViewModel>> GetAllSuppliersAsync()
        {
            try
            {
                var suppliers = await _supplierRepository.GetAllSuppliersAsync();

                if (suppliers == null || !suppliers.Any())  // check for null or empty list
                {
                    _logger.LogWarning("Supplier list is empty.");
                    throw new ArgumentNullException(nameof(suppliers), "Supplier list is empty.");
                }

                return _mapper.Map<IEnumerable<SupplierViewModel>>(suppliers);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, "Supplier list was empty.");
                throw new ApplicationException("Supplier list is empty. Please add suppliers and try again.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching suppliers.");
                throw new ApplicationException("An unexpected error occurred while fetching suppliers.");
            }
        }

        

        public Task<SupplierViewModel?> GetSupplierByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<SupplierViewModel> UpdateSupplierAsync(SupplierViewModel supplier)
        {
            throw new NotImplementedException();
        }
    }
}
