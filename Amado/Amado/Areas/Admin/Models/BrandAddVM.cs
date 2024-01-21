using System.ComponentModel.DataAnnotations;

namespace Amado.Areas.Admin.Models
{
    public class BrandAddVM
    {
        [Required]
        public string Name { get; set; }
    }
}
