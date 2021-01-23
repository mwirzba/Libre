using System;

namespace Libre.Models
{
    public class SearchSettings
    {
        public string SearchString { get; set; } = "";
        public Guid BookGendreId { get; set; } = Guid.Empty;
        public SearchSettings(string searchString, Guid bookGendreId)
        {
            SearchString = searchString;
            BookGendreId = bookGendreId;
        }
    }
}
