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
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public string ISBN { get; set; }

        public int AuthorId { get; set; }
        public List<SelectListItem> Authors { get; set; }
    }
}