using Amado.Data;
using Amado.Entities;
using Amado.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Amado.Controllers
{
    public class SubscribeController : Controller
    {
        private readonly AppDbContext _dbContext;

        public SubscribeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpPost]
        public IActionResult Post(SubscribesComponentVM model)
        {
            if (model is null)
            {
                TempData["SubscribeMessage"] = "Model is null. Email not added.";
                return RedirectToAction("Index", "Home");
            }

            if (!ModelState.IsValid)
            {
                TempData["SubscribeMessage"] = "Model is not valid. Email not added.";
                return RedirectToAction("Index", "Home");
            }

            var existingSubscribe = _dbContext.Subscriptions.FirstOrDefault(s => s.Email == model.Email);

            if (existingSubscribe != null)
            {
                TempData["SubscribeMessage"] = "This email address is already subscribed.";
                return RedirectToAction("Index", "Home");
            }

            var newSubscribe = new Subscribe
            {
                Email = model.Email
            };
            _dbContext.Subscriptions.Add(newSubscribe);
            _dbContext.SaveChanges();

            TempData["SubscribeMessage"] = "Email added successfully.";
            return RedirectToAction("Index", "Home");
        }
    }
}
