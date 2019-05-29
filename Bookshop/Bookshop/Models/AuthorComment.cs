using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookshop.Models
{
    public class AuthorComment
    {
        public int Id { get; set; }

        [Required]
        public int AuthorId { get; set; }

        public Author Author { get; set; }

        [Display(Name = "Komentarz")]
        [StringLength(2000, ErrorMessage = "Maksymalna dłuość opisu to 2000 znaków")]
        public string Content { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime PublicationDate { get; set; }

        [Required]
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}