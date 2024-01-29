using Amado.Entities;
using X.PagedList;

namespace Amado.Models
{
    public class ProductIndexVM
    {
        public IPagedList<Product> Products { get; set; }
    }
}
