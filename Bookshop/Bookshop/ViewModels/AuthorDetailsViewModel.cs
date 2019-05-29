using Bookshop.Models;
using System.ComponentModel.DataAnnotations;

namespace Bookshop.ViewModels
{
    public class AuthorDetailsViewModel
    {
        public Author Author { get; set; }

        [Display(Name = "Komentarz")]
        [StringLength(2000, ErrorMessage = "Maksymalna dłuość opisu to 2000 znaków")]
        public string CommentContent { get; set; }

    }
}