using System;
using System.ComponentModel.DataAnnotations;

namespace Bookshop.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }
        [Display(Name = "Data wydania")]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime PublicationDate { get; set; }
        [Required]
        public string ISBN { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}