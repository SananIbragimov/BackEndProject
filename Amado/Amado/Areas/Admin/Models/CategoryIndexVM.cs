using Amado.Entities;
using X.PagedList;

namespace Amado.Areas.Admin.Models
{
    public class CategoryIndexVM
    {
        public IPagedList<Category> Categories { get; set; }
    }
}
