using AutoMapper;
using ElectricState.Models;
using ElectricState.Repositories;
using ElectricState.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace ElectricState.Controllers
{
    public class ProductController : Controller
    {

        private readonly ILogger<ProductController> _logger;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public ProductController(ILogger<ProductController> logger,IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _logger = logger;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllAsync();
            var productviewModels = _mapper.Map<IEnumerable<ProductViewModel>>(products);

            return View(productviewModels);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var viewModel = new ProductCreateViewModel
            {
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.CategoryName
                }).ToList()
            };
            return View(viewModel);
        }


    
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Categories = (await _categoryRepository.GetAllAsync()).Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.CategoryName
                });
                _logger.LogError("Model State Invalid");
                return View(vm);
            }

            try
            {
                if (await _productRepository.ExistByNameAsync(vm.ProductName))
                {
                    ModelState.AddModelError(nameof(vm.ProductName), "Product name already Exisit");
                    vm.Categories = (await _categoryRepository.GetAllAsync()).Select(c => new SelectListItem
                    {
                        Value = c.CategoryId.ToString(),
                        Text = c.CategoryName
                    });

                    return View(vm);
                }

                var product = _mapper.Map<Product>(vm);
                await _productRepository.CreateProductAsync(product);
                _logger.LogInformation("Added Succesfully");
                return RedirectToAction("Index", "Product");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating product: {@ProductCreateViewModel}", vm);
                vm.Categories = (await _categoryRepository.GetAllAsync()).Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.CategoryName
                });
                return View(vm);
            }

        }
    }
}
