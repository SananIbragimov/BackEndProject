using Microsoft.AspNetCore.Mvc;

namespace Amado.Views.Shared.Components.Search
{
    public class SearchViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
