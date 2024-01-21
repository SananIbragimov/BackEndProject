using System.ComponentModel.DataAnnotations;

namespace Amado.Areas.Admin.Models
{
    public class ColorAddVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string HexCode { get; set; }
    }
}
