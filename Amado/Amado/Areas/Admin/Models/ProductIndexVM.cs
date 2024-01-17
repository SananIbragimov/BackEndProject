

using Amado.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Amado.Areas.Admin.Models
{
    public class ProductIndexVM
    {
        public List<Product> Products { get; set; }
        
    }
}
