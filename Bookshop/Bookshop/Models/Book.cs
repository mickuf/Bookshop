using System;
using System.ComponentModel.DataAnnotations;

namespace Bookshop.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
        [Required]
        public string ISBN { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}