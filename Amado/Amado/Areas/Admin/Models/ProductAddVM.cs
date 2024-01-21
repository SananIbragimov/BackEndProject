using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Amado.Areas.Admin.Models
{
    public class ProductAddVM
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public string Desc { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool InStock { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public int ColorId { get; set; }

        public List<IFormFile> Images { get; set; } = new List<IFormFile>();
        public List<SelectListItem>? Categories { get; set; }
        public List<SelectListItem>? Brands { get; set; }
        public List<SelectListItem>? Colors { get; set; }
    }
}
