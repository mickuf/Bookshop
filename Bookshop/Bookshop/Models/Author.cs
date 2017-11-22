using System;
using System.Collections;
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

        [Required(ErrorMessage = "Opis musi zawierać od 50 do 2000 znaków")]
        [StringLength(2000, MinimumLength = 50)]
        [Display(Name = "Opis")]
        public string Description { get; set; }

        public ICollection<Book> Book { get; set; }
    }
}