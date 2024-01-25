using Amado.Data;
using Amado.Entities;
using Amado.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Amado.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly AppDbContext _dbContext;

        public CheckoutController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(CheckoutIndexVM model)
        {
            if (model == null)
            {
                TempData["CheckoutMessage"] = "Model is null.";
                return View(model);
            }
            if (!ModelState.IsValid)
            {
                TempData["CheckoutMessage"] = "Model is not valid.";
                return View(model);
            }

            var newCheckout = new Checkout
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                CompanyName = model.CompanyName,
                CountryName = model.CountryName,
                Town = model.Town,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                ZipCode = model.ZipCode,
                Address = model.Address,
                Comment = model.Comment
            };

            _dbContext.Checkouts.Add(newCheckout);
            _dbContext.SaveChanges();

            TempData["CheckoutMessage"] = "Form checkout added successfully.";
            return RedirectToAction("Index", "Checkout");
        }
    }
}
