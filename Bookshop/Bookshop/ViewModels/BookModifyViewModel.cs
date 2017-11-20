using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bookshop.ViewModels
{
    public class BookModifyViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tytuł musi mieć od 2 do 50 znaków")]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Data musi być zapisana w formacie dd.mm.yyyy")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data wydania")]
        public DateTime PublicationDate { get; set; }

        [Required(ErrorMessage = "ISBN musi mieć od 10 do 17 znaków")]
        [StringLength(17, MinimumLength = 10)]
        public string ISBN { get; set; }

        [Display(Name = "Autor")]
        public int AuthorId { get; set; }
        public List<SelectListItem> Authors { get; set; }
    }
}