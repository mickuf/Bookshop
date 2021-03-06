﻿using Bookshop.Models;
using Bookshop.Repositories;
using Bookshop.Utils;
using Bookshop.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bookshop.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ISearchUtility _searchUtility;
        private readonly IImageFileUtility _imageFileUtility;
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public BookController()
        {
            _bookRepository = new BookRepository(new BookshopDbContext());
            _authorRepository = new AuthorRepository(new BookshopDbContext());
            _commentRepository = new CommentRepository(new BookshopDbContext());
            _searchUtility = new SearchUtilty();
            _imageFileUtility = new ImageFileUtility();
        }

        public BookController(IBookRepository bookRepository, IAuthorRepository authorRepository, ISearchUtility searchUtility, IImageFileUtility imageFileUtility)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _searchUtility = searchUtility;
            _imageFileUtility = imageFileUtility;
        }

        [AllowAnonymous]
        public ActionResult Index(string filter)
        {
            Log.DebugFormat("GET Index with filter: {0}", filter);
            IEnumerable<Book> books = _bookRepository.GetBooks();

            if (books == null)
            {
                Log.Warn("Books list is null!");
                throw new HttpException();
            }

            if (!String.IsNullOrEmpty(filter))
            {
                books = books.Where(b => _searchUtility.IsInsensitiveString(b.Title, filter) ||
                _searchUtility.IsInsensitiveString(b.Author.Name, filter) ||
                _searchUtility.IsInsensitiveString(b.Author.Surname, filter) ||
                _searchUtility.IsInsensitiveString(b.ISBN, filter));
            }

            return View(books);
        }

        public ActionResult Create()
        {
            Log.Debug("GET Create");
            List<SelectListItem> authorsSelectList = _authorRepository.GetAuthorsSelectList().ToList();

            return View("Create",
                new BookModifyViewModel()
                {
                    PublicationDate = DateTime.Today,
                    Authors = authorsSelectList
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
                    Log.DebugFormat("Is model valid: {0}", ModelState.IsValid);

                    HttpPostedFileBase file = Request.Files[0];

                    int bookId = _bookRepository.CreateBook(new Book()
                    {
                        Title = model.Title,
                        PublicationDate = model.PublicationDate,
                        ISBN = model.ISBN,
                        ImagePath = _imageFileUtility.SaveImageFileInPath(file),
                        Description = model.Description,
                        AuthorId = model.AuthorId
                    });

                    return RedirectToAction("Details", new { id = bookId });
                }
                Log.DebugFormat("Is model valid: {0} repopulate authors DropDownList", ModelState.IsValid);

                model.Authors = _authorRepository.GetAuthorsSelectList().ToList();

                return View(model);
            }
            catch (Exception ex)
            {
                Log.Warn("Create book failed!", ex);
                ModelState.AddModelError(string.Empty, "Nie można zapisać zmian, spróbuj ponowanie");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            Log.DebugFormat("GET Edit with id: {0}", id);

            Book book = _bookRepository.GetBookById(id);
            List<SelectListItem> authorsSelectList = _authorRepository.GetAuthorsSelectList().ToList();

            if (book == null)
            {
                Log.Warn("Book or AuthorsSelectList object is null!");
                throw new HttpException();
            }

            return View("Edit",
                new BookModifyViewModel()
                {
                    Id = book.Id,
                    Title = book.Title,
                    PublicationDate = book.PublicationDate,
                    ISBN = book.ISBN,
                    ImagePath = book.ImagePath,
                    Description = book.Description,
                    AuthorId = book.AuthorId,
                    Authors = authorsSelectList
                });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookModifyViewModel model)
        {
            try
            {
                Log.DebugFormat("POST Edit with book: {0} with id: {1} in model", model.Title, model.Id);

                if (ModelState.IsValid)
                {
                    Log.DebugFormat("Is model valid: {0}", ModelState.IsValid);

                    HttpPostedFileBase file = Request.Files[0];

                    if (file == null || file.ContentLength == 0)
                    {
                        Log.Debug("User do not want change picture");

                        _bookRepository.UpdateBook(
                            new Book()
                            {
                                Id = model.Id,
                                Title = model.Title,
                                PublicationDate = model.PublicationDate,
                                ISBN = model.ISBN,
                                ImagePath = model.ImagePath,
                                Description = model.Description,
                                AuthorId = model.AuthorId
                            });
                    }
                    else
                    {
                        _imageFileUtility.DeleteImageFromPath(model.ImagePath);

                        _bookRepository.UpdateBook(
                            new Book()
                            {
                                Id = model.Id,
                                Title = model.Title,
                                PublicationDate = model.PublicationDate,
                                ISBN = model.ISBN,
                                ImagePath = _imageFileUtility.SaveImageFileInPath(file),
                                Description = model.Description,
                                AuthorId = model.AuthorId
                            });
                    }

                    return RedirectToAction("Details", new { id = model.Id });
                }
                Log.DebugFormat("Is model valid: {0} repopulate authors DropDownList", ModelState.IsValid);
                model.Authors = _authorRepository.GetAuthorsSelectList().ToList();
                return View(model);
            }
            catch (Exception ex)
            {
                Log.Warn("Update book failed!", ex);
                ModelState.AddModelError(string.Empty, "Nie można zaktualizować książki, spróbuj ponownie");
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            Log.DebugFormat("GET Delete with id: {0}", id);
            Book book = _bookRepository.GetBookById(id);

            if (book == null)
            {
                Log.Warn("Book object is null!");
                throw new HttpException();
            }

            return View(book);
        }

        [HttpPost]
        public ActionResult Delete(Book book)
        {
            try
            {
                Log.DebugFormat("POST Delete with book: {0} with id: {1}", book.Title, book.Id);

                _imageFileUtility.DeleteImageFromPath(book.ImagePath);

                _bookRepository.DeleteBook(book.Id);
            }
            catch (Exception ex)
            {
                Log.Warn("Delete book failed!", ex);
                ModelState.AddModelError(string.Empty, "Nie można usunąć książki, spróbuj ponownie");
            }
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            Log.DebugFormat("GET Details with id: {0}", id);

            BookDetailsViewModel bookDetails = new BookDetailsViewModel()
            {
                Book = _bookRepository.GetBookById(id)
            };
        
            if (bookDetails.Book == null)
            {
                Log.Warn("Book object is null!");
                throw new HttpException();
            }

            return View(bookDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(BookDetailsViewModel model)
        {
            _commentRepository.CreateComment(new BookComment()
            {
                ApplicationUserId = User.Identity.GetUserId(),
                BookId = model.Book.Id,
                Content = model.CommentContent,
                PublicationDate = DateTime.Now
            });

            return RedirectToAction("Details", "Book", new { id = model.Book.Id });
        }
    }
}