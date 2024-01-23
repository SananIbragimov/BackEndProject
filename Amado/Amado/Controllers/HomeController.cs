using Amado.Data;
using Amado.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Amado.Controllers
{

    public class HomeController : Controller
    {
        private readonly AppDbContext _dbContext;

        public HomeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var products = _dbContext.Products.Include(x => x.Images).ToList();
            if (products is null) return NotFound();

            var model = new HomeIndexVM
            {
                Products = products
            };

            return View(model);
        }
    }
}