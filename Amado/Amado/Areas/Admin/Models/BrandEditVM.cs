using System.ComponentModel.DataAnnotations;

namespace Amado.Areas.Admin.Models
{
    public class BrandEditVM
    {
        [Required]
        public string Name {  get; set; }
    }
}
