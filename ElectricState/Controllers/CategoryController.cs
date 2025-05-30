using ElectricState.Models;
using ElectricState.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ElectricState.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository? _categoryRepository;
        private readonly ILogger<CategoryController> _logger;


        public CategoryController(ICategoryRepository categoryRepository, ILogger<CategoryController> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;


        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var categories = await _categoryRepository.GetAllAsync();
                return View(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load Categories");
                return View("Error", new ErrorViewModel { Message = "Unable to load Categories" });
            }
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            try
            {
                if (await _categoryRepository.ExistByNameAsync(category.CategoryName))
                {
                    ModelState.AddModelError("CategoryName", "Category name already exists.");
                    return View(category);
                }

                TempData["Success"] = "Category Created Successfully";
                await _categoryRepository.CreateAsync(category);
                return RedirectToAction("Index", "Category");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected Error in Create Action for Category : {@Category)", category);
                ModelState.AddModelError(string.Empty, "An error occured while saving the Category");
                return View(category);
            }

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var existingCategory = await _categoryRepository.GetById(id);

                if(existingCategory == null)
                {
                    _logger.LogWarning("Category with ID {Id} not found", id);
                    TempData["Error"] = "Category not found";
                    return RedirectToAction("Index", "Category");

                }

                await _categoryRepository.DeleteAsync(existingCategory);
                return RedirectToAction("Index", "Category");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to Delete Category");
                return RedirectToAction("Index", "Category");



            }
        }



        public async Task<IActionResult> Update(int id)
        {
            try
            {
                var category = await _categoryRepository.GetById(id);
                if(category == null)
                {
                    return NotFound();
                }
                return View(category);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error fetching category with id {Id}", id);
                return RedirectToAction("Index", "Category");
            }

        }

        [HttpPut]

        public async Task<IActionResult> Update(Category category)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                var UpdatedCategoryy = await _categoryRepository.UpdateAsync(category);
                if(UpdatedCategoryy == null)
                {
                    ModelState.AddModelError("","Category not found");
                    return View(category);
                }
                TempData["Success"] = "Category Updated Successfully";
               return RedirectToAction("Index");

            }
            catch(Exception ex)
            {
                _logger.LogError("An Error Occured while updating category {@Category}", category);
                ModelState.AddModelError("", "An Unexpected Error Occured");
                return View(category);
            }
       
        }
    }
}
