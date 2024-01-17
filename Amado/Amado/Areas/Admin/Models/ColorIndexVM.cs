using Amado.Entities;
using X.PagedList;

namespace Amado.Areas.Admin.Models
{
    public class ColorIndexVM
    {
        public IPagedList<Color> Colors { get; set; }
    }
}
