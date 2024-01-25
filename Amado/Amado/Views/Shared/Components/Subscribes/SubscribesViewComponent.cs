using Amado.Models;
using Microsoft.AspNetCore.Mvc;

namespace Amado.Views.Shared.Components.Subscribes
{
    public class SubscribesViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(SubscribesComponentVM model)
        {
            return View(model);
        }
    }
}
