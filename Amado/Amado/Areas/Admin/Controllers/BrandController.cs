using Amado.Areas.Admin.Models;
using Amado.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using System.Drawing.Printing;
using Amado.Entities;

namespace Amado.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class BrandController : Controller
    {
        private readonly AppDbContext _dbContext;

        public BrandController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index(int page = 1, int pageSize = 4)
        {
            var brands = _dbContext.Brands.OrderBy(x => x.Id);
            var pagedList = brands.ToPagedList(page, pageSize);

            var model = new BrandIndexVM
            {
                Brands = pagedList
            };
            return View(model);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(BrandAddVM model)
        {
            if (!ModelState.IsValid) return View("Add", model);

            var newBrand = new Brand
            {
                Name = model.Name.Trim()
            };

            _dbContext.Brands.Add(newBrand);
            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Brand");
        }

        public IActionResult Edit(int? id)
        {
            var brand = _dbContext.Brands.FirstOrDefault(x => x.Id == id);
            if (brand == null) return NotFound();

            var model = new BrandEditVM
            {
                Name = brand.Name
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(int? id, BrandEditVM model)
        {
            if (!ModelState.IsValid) return View("Edit", model);

            var brand = _dbContext.Brands.FirstOrDefault(x => x.Id == id);
            if (brand == null) return NotFound();

            brand.Name = model.Name.Trim();

            _dbContext.Brands.Update(brand);
            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Brand");
        }

        public IActionResult Delete(int? id)
        {
            var brand = _dbContext.Brands.FirstOrDefault(x => x.Id == id);
            if (brand == null) return NotFound();

            _dbContext.Brands.Remove(brand);
            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Brand");
        }
    }
}
