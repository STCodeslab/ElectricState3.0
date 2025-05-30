using ElectricState.Services.Interfaces;
using ElectricState.ViewModels.Supplier;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ElectricState.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ILogger<SupplierController> _logger;
        private readonly ISupplierService? _supplierService;

        public SupplierController(ILogger<SupplierController> logger, ISupplierService supplierService)
        {
            _logger = logger;
            _supplierService = supplierService;

        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SupplierViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Model State Invalid for Supplier creation.");
                return View(vm);
            }
            try
            {
                await _supplierService.AddSupplierAsync(vm);
                return RedirectToAction("Index", "Supplier");
            }
            catch(InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Business rule Violation while creating error");
                ModelState.AddModelError("", ex.Message);
              

            }
            catch (Exception ex)
            {
                               _logger.LogError(ex, "Error occurred while creating supplier: {@Supplier}", vm);
                ModelState.AddModelError("", "An error occurred while creating the supplier. Please try again.");
                
            }
            return View(vm);

        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var supplier = await _supplierService.GetAllSuppliersAsync();
                return View(supplier);

            }
            catch(ApplicationException ex)
            {
                _logger.LogError(ex, "Handled ApplicationException in Controller.Index");
                ModelState.AddModelError("",ex.Message);
                return View(new List<SupplierViewModel>());
            }

            catch(Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occured");
                ModelState.AddModelError("", ex.Message);
                return View(new List<SupplierViewModel>());
            }

        

           
        }
    }
}
