using Amado.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace Amado.Models
{
    public class ShopIndexVM
    {
        public IPagedList<Product> Products { get; set; }
        public List<Category> Categories { get; set; } = new();
        public List<Brand> Brands { get; set; } = new();
        public List<Color> Colors { get; set; } = new();
        public int? SelectedCategoryId { get; set; }
        public string[] SelectedBrands { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public string SortOrder { get; set; }
        public List<SelectListItem> SortOrderList { get; set; }

        public int ShowCount { get; set; }
        public List<SelectListItem> ShowCountList { get; set; }

        public ShopIndexVM()
        {
            SortOrderList = new List<SelectListItem>
        {
            new SelectListItem { Value = "asc", Text = "Ascending" },
            new SelectListItem { Value = "desc", Text = "Descending" }
        };

            ShowCountList = new List<SelectListItem>
        {
            new SelectListItem { Value = "4", Text = "4" },
            new SelectListItem { Value = "8", Text = "8" },
            new SelectListItem { Value = "12", Text = "12" }
        };
        }
    }
}
