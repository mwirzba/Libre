using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libre.Models
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string  Publisher { get; set; }
        public DateTime RealeaseDate { get; set; }
        public string Language  { get; set; }
        public int Pages { get; set; }
        public string  CoverType { get; set; }
        public string Info { get; set; }

        [ForeignKey("Standard")]
        public Guid GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
