using System.ComponentModel.DataAnnotations;

namespace Amado.Models
{
    public class SubscribesComponentVM
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
    }
}
