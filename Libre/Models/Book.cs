using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libre.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Display(Name = "Tytuł")]
        [Required(ErrorMessage = "Tytuł jest wymagany")]
        public string Title { get; set; }

        [Display(Name = "Autor")]
        [Required(ErrorMessage = "Autor jest wymagany")]
        public string  Publisher { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data wydania")]
        public DateTime RealeaseDate { get; set; }


        [Display(Name = "Język")]
        [Required(ErrorMessage = "Język jest wymagany")]
        public string Language  { get; set; }

        [Display(Name = "Liczba stron")]
        [Required(ErrorMessage = "Pole wymagane")]
        public int Pages { get; set; }

        [Display(Name = "Rodzaj okładki")]
        [Required(ErrorMessage = "Pole wymagane")]
        public string  CoverType { get; set; }

        [Display(Name = "Informacja o książce")]
        [Required(ErrorMessage = "Pole wymagane")]
        public string Info { get; set; }

        [ForeignKey("Standard")]
        public Guid GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
