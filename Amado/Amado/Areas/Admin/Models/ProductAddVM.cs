using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Amado.Areas.Admin.Models
{
    public class ProductAddVM
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public int Quantity { get; set; }
        public bool InStock { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public int ColorId { get; set; }

        [Display(Name = "Select Images")]
        public List<IFormFile> Images { get; set; } = new List<IFormFile>();
        public List<SelectListItem>? Categories { get; set; }
        public List<SelectListItem>? Brands { get; set; }
        public List<SelectListItem>? Colors { get; set; }
    }
}
