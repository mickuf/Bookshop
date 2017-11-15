using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bookshop.Repository;
using Bookshop.Models;

namespace Bookshop.Controllers
{
    public class BookController : Controller
    {
        private IBookRepository _bookRepository;

        public BookController()
        {
            _bookRepository = new BookRepository(new DatabaseBookshop());
        }

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        // GET: Book
        public ActionResult Index()
        {
            IEnumerable<Book> Books = _bookRepository.GetBooks();

            return View(Books);
        }
    }
}