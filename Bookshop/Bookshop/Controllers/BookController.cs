using Bookshop.Models;
using Bookshop.Repositories;
using Bookshop.Utils;
using Bookshop.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Bookshop.Controllers
{
    [Authorize(Roles = "Admin")]
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

        [AllowAnonymous]
        public ActionResult Index(string filter)
        {
            _log.DebugFormat("GET Index with filter: {0}", filter);
            IEnumerable<Book> Books = _bookRepository.GetBooks();

            if (Books == null)
            {
                _log.Warn("Books list is null!");
                return new HttpNotFoundResult();
            }

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
            _log.Debug("GET Create");
            List<SelectListItem> AuthorsSelectList = _authorRepository.GetAuthorsSelectList().ToList();

            if (AuthorsSelectList == null)
            {
                _log.Warn("Books list is null!");
                return new HttpNotFoundResult();
            }
                

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
                    _log.DebugFormat("Is model valid: {0}", ModelState.IsValid);
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
                _log.DebugFormat("Is model valid: {0} repopulate authors DropDownList", ModelState.IsValid);
                model.Authors = _authorRepository.GetAuthorsSelectList().ToList();
                return View(model);
            }
            catch (Exception ex)
            {
                _log.Warn("Create book failed!", ex);
                ModelState.AddModelError(string.Empty, "Nie można zapisać zmian, spróbuj ponowanie");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            _log.DebugFormat("GET Edit with id: {0}", id);

            Book book = _bookRepository.GetBookById(id);
            List<SelectListItem> AuthorsSelectList = _authorRepository.GetAuthorsSelectList().ToList();

            if (book == null || AuthorsSelectList == null)
            {
                _log.Warn("Book or AuthorsSelectList object is null!");
                return new HttpNotFoundResult();
            }

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
                _log.DebugFormat("POST Edit with book: {0} with id: {1} in model", model.Title, model.Id);

                if (ModelState.IsValid)
                {
                    _log.DebugFormat("Is model valid: {0}", ModelState.IsValid);

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
                _log.DebugFormat("Is model valid: {0} repopulate authors DropDownList", ModelState.IsValid);
                model.Authors = _authorRepository.GetAuthorsSelectList().ToList();
                return View(model);
            }
            catch (Exception ex)
            {
                _log.Warn("Update book failed!", ex);
                ModelState.AddModelError(string.Empty, "Nie można zaktualizować książki, spróbuj ponownie");
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            _log.DebugFormat("GET Delete with id: {0}", id);
            Book book = _bookRepository.GetBookById(id);

            if (book == null)
            {
                _log.Warn("Book object is null!");
                return new HttpNotFoundResult();
            }

            return View(book);
        }

        [HttpPost]
        public ActionResult Delete(Book book)
        {
            try
            {
                _log.DebugFormat("POST Delete with book: {0} with id: {1}", book.Title, book.Id);

                _bookRepository.DeleteBook(book.Id);
            }
            catch (Exception ex)
            {
                _log.Warn("Delete book failed!", ex);
                ModelState.AddModelError(string.Empty, "Nie można usunąć książki, spróbuj ponownie");
            }
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            _log.DebugFormat("GET Details with id: {0}", id);
            Book book = _bookRepository.GetBookById(id);

            if (book == null)
            {
                _log.Warn("Book object is null!");
                return new HttpNotFoundResult();
            }           

            return View(book);
        }
    }
}