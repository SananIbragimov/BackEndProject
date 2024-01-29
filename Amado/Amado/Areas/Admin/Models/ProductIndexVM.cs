

using Amado.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace Amado.Areas.Admin.Models
{
    public class ProductIndexVM
    {
        public IPagedList<Product> Products { get; set; }
        
    }
}
