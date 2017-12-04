using System;
using System.ComponentModel.DataAnnotations;

namespace Bookshop.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }
 
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data wydania")]
        [Range(typeof(DateTime), "1/1/1700", "1/1/2100",
            ErrorMessage = "Poprawna data to data między {1} i {2}")]
        public DateTime PublicationDate { get; set; }

        [Required]
        [StringLength(17, MinimumLength = 10)]
        public string ISBN { get; set; }

        [Display(Name = "Opis")]
        [StringLength(2000, ErrorMessage = "Maksymalna dłuość opisu to 2000 znaków")]
        public string Description { get; set; }

        [Display(Name = "Obrazek")]
        public string ImagePath { get; set; }

        public int AuthorId { get; set; }

        [Display(Name = "Autor")]
        public Author Author { get; set; }
    }
}