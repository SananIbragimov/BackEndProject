using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Amado.Areas.Admin.Models
{
    public class CategoryAddVM
    {
        [Required]
        public string Name { get; set; }
    }
}
