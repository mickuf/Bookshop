using Bookshop.Models;
using Bookshop.Repository;
using Bookshop.Utils;
using Bookshop.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Bookshop.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ISearchUtility _searchUtility;
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public BookController()
        {
            _bookRepository = new BookRepository(new BookshopDbContext());
            _authorRepository = new AuthorRepository(new BookshopDbContext());
            _searchUtility = new SearchUtilty();
        }

        public BookController(IBookRepository bookRepository, IAuthorRepository authorRepository, ISearchUtility searchUtility)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _searchUtility = searchUtility;
        }

        public ActionResult Index(string filter)
        {
            IEnumerable<Book> Books = _bookRepository.GetBooks();

            if (Books == null)
                return new HttpNotFoundResult();

            if (!String.IsNullOrEmpty(filter))
            {
                Books = Books.Where(b => _searchUtility.IsInsensitiveString(b.Title, filter) ||
                _searchUtility.IsInsensitiveString(b.Author.Name, filter) ||
                _searchUtility.IsInsensitiveString(b.Author.Surname, filter) ||
                _searchUtility.IsInsensitiveString(b.ISBN, filter));
            }

            return View(Books);
        }

        public ActionResult Create()
        {
            List<SelectListItem> AuthorsSelectList = _authorRepository.GetAuthorsSelectList().ToList();

            if (AuthorsSelectList == null)
                return new HttpNotFoundResult();

            return View("Create",
                new BookModifyViewModel()
                {
                    PublicationDate = DateTime.Today,
                    Authors = AuthorsSelectList
                });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookModifyViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _bookRepository.CreateBook(new Book()
                    {
                        Title = model.Title,
                        PublicationDate = model.PublicationDate,
                        ISBN = model.ISBN,
                        Description = model.Description,
                        AuthorId = model.AuthorId
                    });

                    return RedirectToAction("Index");
                }
                model.Authors = _authorRepository.GetAuthorsSelectList().ToList();
                return View(model);
            }
            catch (DataException dex)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator. \n " + dex);
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            Book book = _bookRepository.GetBookById(id);
            List<SelectListItem> AuthorsSelectList = _authorRepository.GetAuthorsSelectList().ToList();

            if (book == null || AuthorsSelectList == null)
                return new HttpNotFoundResult();

            return View("Edit",
                new BookModifyViewModel()
                {
                    Id = book.Id,
                    Title = book.Title,
                    PublicationDate = book.PublicationDate,
                    ISBN = book.ISBN,
                    Description = book.Description,
                    AuthorId = book.AuthorId,
                    Authors = AuthorsSelectList
                });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookModifyViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int bookId = model.Id;

                    _bookRepository.UpdateBook(
                        new Book()
                        {
                            Id = model.Id,
                            Title = model.Title,
                            PublicationDate = model.PublicationDate,
                            ISBN = model.ISBN,
                            Description = model.Description,
                            AuthorId = model.AuthorId
                        });

                    return RedirectToAction("Index");
                }
                model.Authors = _authorRepository.GetAuthorsSelectList().ToList();
                return View(model);
            }
            catch (DataException dex)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.\n" + dex);
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            Book book = _bookRepository.GetBookById(id);

            if (book == null)
                return new HttpNotFoundResult();

            return View(book);
        }

        [HttpPost]
        public ActionResult Delete(Book book)
        {
            try
            {
                _bookRepository.DeleteBook(book.Id);
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                //return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            Book book = _bookRepository.GetBookById(id);

            if (book == null)
                return new HttpNotFoundResult();

            return View(book);
        }
    }
}