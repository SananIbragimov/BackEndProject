using Amado.Areas.Admin.Models;
using Amado.Data;
using Amado.Entities;
using Amado.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Globalization;
using X.PagedList;

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

        public IActionResult Index(int page = 1, int pageSize = 5)
        {
            var products = _dbContext.Products
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .Include(x => x.Color)
                .Include(x => x.Images).OrderBy(x => x.Id);

            var pagedList = products.ToPagedList(page, pageSize);

            var model = new ProductIndexVM { Products = pagedList };

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
        public IActionResult Add(ProductAddVM viewModel, [FromServices] IEmailService emailService)
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
                Price = viewModel.Price,
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

            var subscribers = _dbContext.Subscriptions.Select(s => s.Email).ToList();
            string subject = "New product";
            string body = $"Added new product: {newProduct.Name}";

            foreach (var subscriber in subscribers)
            {
                emailService.SendMail(subscriber, subject, body);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var product = _dbContext.Products.Include(p => p.Images).FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            var productEditVM = new ProductEditVM
            {
                Id = product.Id,
                Name = product.Name,
                Desc = product.Desc,
                Price = Decimal.Parse(product.Price.ToString("N2")),
                Quantity = product.Quantity,
                InStock = product.InStock,
                CategoryId = product.CategoryId,
                BrandId = product.BrandId,
                ColorId = product.ColorId,
                Categories = GetCategories(),
                Brands = GetBrands(),
                Colors = GetColors(),
                ExistingImages = product.Images.Select(i => i.ImgUrl).ToList()
            };
            Console.WriteLine("Existing Images Count: " + productEditVM.ExistingImages.Count);

            return View(productEditVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ProductEditVM model)
        {

            if (ModelState.IsValid)
            {

                var updatedProduct = new Product
                {
                    Id = model.Id,
                    Name = model.Name,
                    Desc = model.Desc,
                    Price = model.Price,
                    Quantity = model.Quantity,
                    InStock = model.InStock,
                    CategoryId = model.CategoryId,
                    BrandId = model.BrandId,
                    ColorId = model.ColorId

                };

                var removedImages = model.RemovedImages?.Split(',');

                if (model.RemovedImages != null && model.RemovedImages.Any())
                {
                    var cleanedRemovedImages = model.RemovedImages.Select(url => url.Trim()).ToList();

                    foreach (var imageUrl in cleanedRemovedImages)
                    {
                        var fileName = GetFileNameFromUrl(imageUrl);
                        var imageToRemove = _dbContext.Images.FirstOrDefault(i => i.ImgUrl.Trim() == fileName);

                        if (imageToRemove != null)
                        {
                            _fileUploadService.DeleteFile(imageToRemove.ImgUrl, Path.Combine("img", "featured"));

                            _dbContext.Images.Remove(imageToRemove);
                        }
                    }

                    _dbContext.SaveChanges();
                }




                if (model.Images != null && model.Images.Count > 0)
                {
                    updatedProduct.Images = new List<Image>();

                    foreach (var formFile in model.Images)
                    {
                        var productImage = new Image
                        {
                            ImgUrl = _fileUploadService.AddFile(formFile, Path.Combine("img", "featured")),
                        };
                        updatedProduct.Images.Add(productImage);
                    }
                }

                _dbContext.Products.Update(updatedProduct);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }


            model.Categories = GetCategories();
            model.Brands = GetBrands();
            model.Colors = GetColors();
            return View(model);
        }


        private List<SelectListItem> GetCategories()
        {

            var categories = _dbContext.Categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            return categories;
        }

        private List<SelectListItem> GetBrands()
        {

            var brands = _dbContext.Brands.Select(b => new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = b.Name
            }).ToList();

            return brands;
        }

        private List<SelectListItem> GetColors()
        {

            var colors = _dbContext.Colors.Select(cl => new SelectListItem
            {
                Value = cl.Id.ToString(),
                Text = cl.Name
            }).ToList();

            return colors;
        }
        public static string GetFileNameFromUrl(string url)
        {
            var uri = new Uri(url);
            return Path.GetFileName(uri.LocalPath);
        }



        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var product = _dbContext.Products.Include(x => x.Images).FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            foreach (var image in product.Images)
            {
                _fileUploadService.DeleteFile(image.ImgUrl, Path.Combine("img", "featured"));
            }

            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


    }
}
