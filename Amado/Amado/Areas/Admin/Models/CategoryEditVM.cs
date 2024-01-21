using System.ComponentModel.DataAnnotations;

namespace Amado.Areas.Admin.Models
{
    public class CategoryEditVM
    {
        [Required]
        public string Name { get; set; }
    }
}
