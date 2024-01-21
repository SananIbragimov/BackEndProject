using System.ComponentModel.DataAnnotations;

namespace Amado.Areas.Admin.Models
{
    public class SubscribeEditVM
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
    }
}
