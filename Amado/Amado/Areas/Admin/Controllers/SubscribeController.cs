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
    public class SubscribeController : Controller
    {
        private readonly AppDbContext _dbContext;

        public SubscribeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index(int page = 1, int pageSize = 4)
        {
            var subscribes = _dbContext.Subscriptions.OrderBy(x => x.Id);
            var pagedList = subscribes.ToPagedList(page, pageSize);

            var model = new SubscribeIndexVM
            {
                Subscribes = pagedList
            };
            return View(model);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(SubscribeAddVM model)
        {
            if (!ModelState.IsValid) return View("Add", model);

            var newSubscribe = new Subscribe
            {
                Email = model.Email.Trim().ToLower()
            };

            _dbContext.Subscriptions.Add(newSubscribe);
            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Subscribe");
        }

        public IActionResult Edit(int? id)
        {
            var subscribe = _dbContext.Subscriptions.FirstOrDefault(x => x.Id == id);
            if (subscribe == null) return NotFound();

            var model = new SubscribeEditVM
            {
                Email = subscribe.Email,
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(int? id, SubscribeEditVM model)
        {
            if (!ModelState.IsValid) return View("Edit", model);

            var subscribe = _dbContext.Subscriptions.FirstOrDefault(x => x.Id == id);
            if (subscribe == null) return NotFound();

            subscribe.Email = model.Email.Trim().ToLower();

            _dbContext.Subscriptions.Update(subscribe);
            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Subscribe");
        }

        public IActionResult Delete(int? id)
        {
            var subscribe = _dbContext.Subscriptions.FirstOrDefault(x => x.Id == id);
            if (subscribe == null) return NotFound();

            _dbContext.Subscriptions.Remove(subscribe);
            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Subscribe");
        }
    }
}
