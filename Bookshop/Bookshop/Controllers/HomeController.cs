using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bookshop.Models;

namespace Bookshop.Controllers
{
    public class HomeController : Controller
    {
        DatabaseBookshop database = new DatabaseBookshop();

        //// GET: Home
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public string Index()
        {
            //database.TestDate();

            Book book = database.Books.FirstOrDefault();
            return book.Id + book.Title + book.AuthorId + book.Author + book.ISBN + book.PublicationDate + database.Database.Connection.ConnectionString;
        }
    }
}