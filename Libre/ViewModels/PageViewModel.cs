using Libre.Helpers;
using X.PagedList;

namespace Libre.ViewModels
{
    public class PageViewModel<T>
    {
        public PagedList<T> Items { get; set; }
        public SortType SortType { get; set; }
        public int CurrentPage { get; set; }

        public string Search { get; set; }
    }
}
