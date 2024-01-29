using Amado.Data;
using Amado.Entities;
using Amado.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Amado.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _dbContext;

        public ShopController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index(int? categoryId, string[] selectedBrands, int? colorId, int? minPrice, int? maxPrice, int page = 1, int pageSize = 6, string sortOrder = "asc", int showCount = 4)
        {
            var products = _dbContext.Products.AsNoTracking();

            if (categoryId.HasValue)
            {
                products = products.Where(x => x.CategoryId == categoryId);
            }
            if (selectedBrands != null && selectedBrands.Any())
            {
                products = products.Where(x => selectedBrands.Contains(x.BrandId.ToString()));
            }

            if (colorId.HasValue)
            {
                products = products.Where(x => x.ColorId == colorId);
            }

            if (minPrice.HasValue && maxPrice.HasValue)
            {
                products = products.Where(x => x.Price >= minPrice && x.Price <= maxPrice);
            }


            switch (sortOrder)
            {
                case "asc":
                    products = products.OrderBy(x => x.Id);
                    break;
                case "desc":
                    products = products.OrderByDescending(x => x.Id);
                    break;
                default:
                    break;
            }

            pageSize = showCount == 4 ? 4 : (showCount == 8 ? 8 : 12);

            var pagedList = products.ToPagedList(page, pageSize);
            var categories = _dbContext.Categories.AsNoTracking().ToList();
            var brands = _dbContext.Brands.AsNoTracking().ToList();
            var colors = _dbContext.Colors.AsNoTracking().ToList();

            if (pagedList == null || categories == null || brands == null || colors == null) { return NotFound(); }

            var model = new ShopIndexVM
            {
                Products = pagedList,
                Categories = categories,
                Brands = brands,
                Colors = colors,
                SortOrder = sortOrder,
                ShowCount = showCount,
                SelectedCategoryId = categoryId,
                SelectedBrands = selectedBrands,
                MinPrice = minPrice ?? 10,
                MaxPrice = maxPrice ?? 1000
            };
            return View(model);
        }

        public IActionResult Search(string? input)
        {
            var products = input == null ? new List<Product>()
                        : _dbContext.Products.Where(x => x.Name.ToLower()
                            .StartsWith(input.ToLower()))
                            .ToList();

            return ViewComponent("SearchResult", products);
        }

    }
}
