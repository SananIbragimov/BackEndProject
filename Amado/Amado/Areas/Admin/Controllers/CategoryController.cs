using Amado.Areas.Admin.Models;
using Amado.Data;
using Amado.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Amado.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _dbContext;

        public CategoryController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var categories = _dbContext.Categories.ToList();
            if (categories == null)  return NotFound();

            var model = new CategoryIndexVM
            {
                Categories = categories
            };
            return View(model);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(CategoryAddVM model)
        {
            if(!ModelState.IsValid) return View("Add", model);

            var newCategory = new Category
            {
                Name = model.Name.Trim()
            };

            _dbContext.Categories.Add(newCategory);
            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Category");
        }

        public IActionResult Edit(int? id)
        {
            var category = _dbContext.Categories.FirstOrDefault(x => x.Id == id);
            if (category == null) return NotFound();

            var model = new CategoryEditVM
            {
                Name = category.Name
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(int? id, CategoryEditVM model)
        {
            if (!ModelState.IsValid) return View("Edit", model);

            var product = _dbContext.Categories.FirstOrDefault(x => x.Id == id);
            if (product == null) return NotFound();

            product.Name = model.Name.Trim();

            _dbContext.Categories.Update(product);
            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Category");
        }

        public IActionResult Delete(int? id)
        {
            var product = _dbContext.Categories.FirstOrDefault(x => x.Id == id);
            if(product == null) return NotFound();

            _dbContext.Categories.Remove(product);
            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Category");
        }
    }
}
