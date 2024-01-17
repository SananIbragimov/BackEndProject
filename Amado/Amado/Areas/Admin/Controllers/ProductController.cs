using Amado.Areas.Admin.Models;
using Amado.Data;
using Amado.Entities;
using Amado.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Amado.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly FileUploadService _fileUploadService;

        public ProductController(AppDbContext dbContext, FileUploadService fileUploadService)
        {
            _dbContext = dbContext;
            _fileUploadService = fileUploadService;
        }

        public IActionResult Index()
        {
            var products = _dbContext.Products
                .Include(x=>x.Category)
                .Include(x => x.Brand)
                .Include(x => x.Color)
                .Include(x=>x.Images).ToList();

            var model = new ProductIndexVM { Products = products };

            return View(model);
        }

        public IActionResult Add()
        {
            var viewModel = new ProductAddVM
            {
                Categories = _dbContext.Categories
        .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
        .ToList(),
                Brands = _dbContext.Brands
        .Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Name })
        .ToList(),
                Colors = _dbContext.Colors
        .Select(cl => new SelectListItem { Value = cl.Id.ToString(), Text = cl.Name })
        .ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Add(ProductAddVM viewModel, FileUploadService fileUploadService)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);

                foreach (var error in errors)
                {
                    Debug.WriteLine(error.ErrorMessage);
                }
                
                viewModel.Categories = _dbContext.Categories
                    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                    .ToList();
                viewModel.Brands = _dbContext.Brands
                    .Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Name })
                    .ToList();
                viewModel.Colors = _dbContext.Colors
                    .Select(cl => new SelectListItem { Value = cl.Id.ToString(), Text = cl.Name, })
                    .ToList();

                return View(viewModel);
            }

            
            var newProduct = new Product
            {
                Name = viewModel.Name,
                Desc = viewModel.Desc,
                Quantity = viewModel.Quantity,
                InStock = viewModel.InStock,
                CategoryId = viewModel.CategoryId,
                BrandId = viewModel.BrandId,
                ColorId = viewModel.ColorId
                
            };

            if (viewModel.Images != null && viewModel.Images.Count > 0)
            {
                newProduct.Images = new List<Image>();

                foreach (var formFile in viewModel.Images)
                {
                    var productImage = new Image
                    {
                        ImgUrl = _fileUploadService.AddFile(formFile, Path.Combine("img", "featured")), 
                    };
                    newProduct.Images.Add(productImage);
                }
                }
                    
                    _dbContext.Products.Add(newProduct);
            _dbContext.SaveChanges();

            return RedirectToAction("Index"); 
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var image = _dbContext.Images.Include(x=>x.Product).FirstOrDefault(x=>x.ProductId == id);

            if (image == null)
            {
                return NotFound();
            }

            _fileUploadService.DeleteFile(image.ImgUrl, Path.Combine("img", "featured"));

            // Ürünü sil
            _dbContext.Products.Remove(image.Product);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


    }
}
