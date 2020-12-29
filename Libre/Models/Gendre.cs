using System;
using System.Collections.Generic;

namespace Libre.Models
{
    public class Gendre
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
