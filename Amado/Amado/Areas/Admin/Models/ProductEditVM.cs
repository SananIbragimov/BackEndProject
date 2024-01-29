using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Amado.Areas.Admin.Models
{
    public class ProductEditVM
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public string Desc { get; set; }
        [Required]
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
        public List<string> ExistingImages { get; set; } = new List<string>();
        public List<string> RemovedImages { get; set; } = new List<string>();

    }
}
