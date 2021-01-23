using System.Collections.Generic;

namespace Libre.Models
{
    public class ListViewModel<T>
    {
        public IEnumerable<T> Items { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
