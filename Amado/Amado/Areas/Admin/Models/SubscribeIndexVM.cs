using Amado.Entities;
using X.PagedList;

namespace Amado.Areas.Admin.Models
{
    public class SubscribeIndexVM
    {
        public IPagedList<Subscribe> Subscribes { get; set; }
    }
}
