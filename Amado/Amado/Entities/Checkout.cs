using System.ComponentModel.DataAnnotations;

namespace Amado.Entities
{
    public class Checkout
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "FirstName is required")]
        [MaxLength(255, ErrorMessage = "FirstName can't be longer than 255 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        [MaxLength(255, ErrorMessage = "LastName can't be longer than 255 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "CompanyName is required")]
        [MaxLength(100, ErrorMessage = "CompanyName can't be longer than 100 characters")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "CountryName is required")]
        [MaxLength(50, ErrorMessage = "CountryName can't be longer than 50 characters")]
        public string CountryName { get; set; }

        [Required(ErrorMessage = "Town is required")]
        [MaxLength(100, ErrorMessage = "Town can't be longer than 100 characters")]
        public string Town { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [MaxLength(255, ErrorMessage = "Email can't be longer than 255 characters")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required")]
        [MaxLength(50, ErrorMessage = "PhoneNumber can't be longer than 50 characters")]
        [Phone(ErrorMessage = "Invalid phone format")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "ZipCode is required")]
        [MaxLength(50, ErrorMessage = "ZipCode can't be longer than 50 characters")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [MaxLength(100, ErrorMessage = "Address can't be longer than 100 characters")]
        public string Address { get; set; }

        [MaxLength(500, ErrorMessage = "Address can't be longer than 100 characters")]
        public string Comment { get; set; }

    }
}
