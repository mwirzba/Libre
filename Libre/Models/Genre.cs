using System;
using System.Collections.Generic;

namespace Libre.Models
{
    public class Genre
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
