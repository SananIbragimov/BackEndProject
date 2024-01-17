using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Amado.Entities
{
    public class Subscribe
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [MaxLength(255, ErrorMessage = "Email can't be longer than 255 characters")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
    }
}
