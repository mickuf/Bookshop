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
        [Display(Name = "Tytuł")]
        public string Title { get; set; }
        [Display(Name = "Data wydania")]
        public DateTime PublicationDate { get; set; }
        public string ISBN { get; set; }

        [Display(Name = "Autor")]
        public int AuthorId { get; set; }
        public List<SelectListItem> Authors { get; set; }
    }
}