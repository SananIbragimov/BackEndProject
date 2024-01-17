using Amado.Areas.Admin.Models;
using Amado.Data;
using Amado.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace Amado.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ColorController : Controller
    {
        private readonly AppDbContext _dbContext;

        public ColorController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index(int page = 1, int pageSize = 4)
        {
            var colors = _dbContext.Colors.OrderBy(x => x.Id);
            var pagedList = colors.ToPagedList(page, pageSize);

            var model = new ColorIndexVM
            {
                Colors = pagedList
            };
            return View(model);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ColorAddVM model)
        {
            if (!ModelState.IsValid) return View("Add", model);

            var newColor = new Color
            {
                Name = model.Name.Trim().ToLower()
            };

            _dbContext.Colors.Add(newColor);
            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Color");
        }

        public IActionResult Edit(int? id)
        {
            var color = _dbContext.Colors.FirstOrDefault(x => x.Id == id);
            if (color == null) return NotFound();

            var model = new ColorEditVM
            {
                Name = color.Name
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(int? id, ColorEditVM model)
        {
            if (!ModelState.IsValid) return View("Edit", model);

            var color = _dbContext.Colors.FirstOrDefault(x => x.Id == id);
            if (color == null) return NotFound();

            color.Name = model.Name.Trim().ToLower();

            _dbContext.Colors.Update(color);
            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Color");
        }

        public IActionResult Delete(int? id)
        {
            var color = _dbContext.Colors.FirstOrDefault(x => x.Id == id);
            if (color == null) return NotFound();

            _dbContext.Colors.Remove(color);
            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Color");
        }
    }
}
