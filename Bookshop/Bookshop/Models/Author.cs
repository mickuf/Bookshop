using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bookshop.Models
{
    public class Author
    {
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "Imię musi mieć od 2 do 20 znaków")]
        [StringLength(20, MinimumLength = 2)]
        [Display(Name = "Imię")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Nazwisko musi mieć od 2 do 20 znaków")]
        [StringLength(20, MinimumLength = 2)]
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        [Display(Name = "Opis")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Obrazek")]
        public string ImagePath { get; set; }

        [Display(Name = "Książki")]
        public ICollection<Book> Book { get; set; }
    }
}