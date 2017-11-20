using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bookshop.Repository;
using Bookshop.Models;
using System.Data;
using Bookshop.ViewModels;

namespace Bookshop.Controllers
{
    public class BookController : Controller
    {
        private IBookRepository _bookRepository;
        private IAuthorRepository _authorRepository;

        public BookController()
        {
            _bookRepository = new BookRepository(new DatabaseBookshop());
            _authorRepository = new AuthorRepository(new DatabaseBookshop());
        }

        public BookController(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        private List<SelectListItem> GetAuthorsSelectList()
        {
            List<SelectListItem> authors = new List<SelectListItem>();

            foreach (Author author in _authorRepository.GetAuthors())
            {
                authors.Add(new SelectListItem()
                {
                    Text = author.Name + " " + author.Surname,
                    Value = author.AuthorId.ToString()
                });
            }

            return authors;
        }

        // GET: Book
        public ActionResult Index()
        {
            IEnumerable<Book> Books = _bookRepository.GetBooks();

            return View(Books);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            return View("Create",
                new BookModifyViewModel()
                {
                    Authors = GetAuthorsSelectList()
                });
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,PublicationDate,ISBN,AuthorId")] BookModifyViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _bookRepository.InsertBook(new Book()
                    {
                        Title = model.Title,
                        PublicationDate = model.PublicationDate,
                        ISBN = model.ISBN,
                        AuthorId = model.AuthorId
                    });

                    _bookRepository.Save();

                    return RedirectToAction("Index");
                }
                model.Authors = GetAuthorsSelectList();
                return View(model);
            }
            catch (DataException dex)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator. \n " + dex);
            }
            return View();
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int id)
        {
            Book book = _bookRepository.GetBookById(id);

            return View("Edit",
                new BookModifyViewModel()
                {
                    Id = book.Id,
                    Title = book.Title,
                    PublicationDate = book.PublicationDate,
                    ISBN = book.ISBN,
                    AuthorId = book.AuthorId,
                    Authors = GetAuthorsSelectList()
                });
        }

        // POST: Book/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,PublicationDate,ISBN,AuthorId")] BookModifyViewModel model)
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
                            AuthorId = model.AuthorId
                        });

                    _bookRepository.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException dex)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.\n" + dex);
            }
            return View();
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int id)
        {
            Book book = _bookRepository.GetBookById(id);

            if (book == null)
                return new HttpNotFoundResult();

            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost]
        public ActionResult Delete(Book book)
        {
            try
            {
                _bookRepository.DeleteBook(book.Id);
                _bookRepository.Save();
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                //return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        // GET: Book/Details/5
        public ViewResult Details(int id)
        {
            Book book = _bookRepository.GetBookById(id);
            return View(book);
        }
    }
}