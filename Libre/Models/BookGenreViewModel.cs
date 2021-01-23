using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Libre.Models
{
    public class BookGenreViewModel
    {
        public ListViewModel<Book> Books { get; set; }
        public List<Genre> Genres { get; set; }
        public Guid BookGenre { get; set; }
        public string SearchString { get; set; }
    }
}
