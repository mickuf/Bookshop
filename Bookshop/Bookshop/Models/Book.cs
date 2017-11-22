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
        public DateTime PublicationDate { get; set; }

        [Required]
        [StringLength(17, MinimumLength = 10)]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "Opis musi zawierać od 50 do 2000 znaków")]
        [StringLength(2000, MinimumLength = 50)]
        [Display(Name = "Opis")]
        public string Description { get; set; }

        public int AuthorId { get; set; }

        [Display(Name = "Autor")]
        public Author Author { get; set; }
    }
}