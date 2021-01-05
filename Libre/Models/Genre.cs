using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libre.Models
{
    public class Genre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        [Display(Name = "Nazwa Gatunku")]
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
