using Amado.Data;
using Amado.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Amado.Controllers
{
    public class BasketController : Controller
    {
        private readonly AppDbContext _dbContext;

        public BasketController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            Request.Cookies.TryGetValue("basket", out var basketSerialized);

            Basket basket = null;

            if (basketSerialized is null)
            {
                basket = new Basket();
            }
            else
            {
                basket = JsonSerializer.Deserialize<Basket>(basketSerialized);
            }

            return View(basket);
        }

        [HttpPost]
        public IActionResult AddToBasket(int? id)
        {
            if (id == null) return NotFound();

            var foundProduct = _dbContext.Products.Include(x => x.Images).FirstOrDefault(p => p.Id == id);
            if (foundProduct == null) return NotFound();

            Request.Cookies.TryGetValue("basket", out var basketSerialized);

            Basket basket = null;

            if (basketSerialized is null)
            {
                basket = new Basket();
            }
            else
            {
                basket = JsonSerializer.Deserialize<Basket>(basketSerialized);
            }

            var foundBasketItem = basket.BasketItems.FirstOrDefault(x => x.ProductId == foundProduct.Id);
            if (foundBasketItem == null)
            {
                foundBasketItem = new BasketItem
                {
                    ProductId = foundProduct.Id,
                    Name = foundProduct.Name,
                    Price = foundProduct.Price,
                    Quantity = 1,
                    ImageUrl = foundProduct.Images.First().ImgUrl
                };
                basket.BasketItems.Add(foundBasketItem);
            }
            else
            {
                foundBasketItem.Quantity++;
                foundBasketItem.Price = foundProduct.Price * foundBasketItem.Quantity;
            }

            Response.Cookies.Append("basket", JsonSerializer.Serialize(basket));

            return RedirectToAction(nameof(Index));
        }
    }
}
