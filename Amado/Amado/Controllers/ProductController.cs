using Amado.Data;
using Amado.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Amado.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _dbContext;

        public ProductController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index(int page = 1, int pageSize = 12)
        {
            var products = _dbContext.Products.Include(x => x.Images).OrderBy(x=>x.Id);
            var pagedList = products.ToPagedList(page, pageSize);

            if (products is null) return NotFound();

            var model = new ProductIndexVM
            {
                Products = pagedList
            };

            return View(model);
        }

        public IActionResult Detail(int id)
        {
            if (id == 0) return NotFound();

            var product = _dbContext.Products.Include(x => x.Images).Include(x => x.Category).Include(x => x.Brand).Include(x => x.Color).FirstOrDefault(x => x.Id == id);
            if (product is null) return NotFound();

            var model = new ProductDetailVM
            {
                Product = product
            };

            return View(model);
        }
    }
}
