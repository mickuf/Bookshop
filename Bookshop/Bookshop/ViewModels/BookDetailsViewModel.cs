using Bookshop.Models;
using System.ComponentModel.DataAnnotations;

namespace Bookshop.ViewModels
{
    public class BookDetailsViewModel
    {
        public Book Book { get; set; }

        [Display(Name = "Komentarz")]
        [StringLength(2000, ErrorMessage = "Maksymalna dłuość opisu to 2000 znaków")]
        public string CommentContent { get; set; }
    }
}