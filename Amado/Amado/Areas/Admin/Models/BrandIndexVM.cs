using Amado.Entities;
using X.PagedList;

namespace Amado.Areas.Admin.Models
{
    public class BrandIndexVM
    {
        public IPagedList<Brand> Brands { get; set; }
    }
}
