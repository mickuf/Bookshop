using Bookshop.Models;
using Bookshop.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Bookshop.Controllers
{
    public class AuthorController : Controller
    {
        private IAuthorRepository _authorRepository;

        public AuthorController()
        {
            _authorRepository = new AuthorRepository(new BookshopDbContext());
        }

        public AuthorController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        private bool IsInsensitiveString(string value, string filter)
        {
            return value.IndexOf(filter, StringComparison.CurrentCultureIgnoreCase) != -1;
        }

        // GET: Author
        public ActionResult Index(string filter)
        {
            IEnumerable<Author> Authors = _authorRepository.GetAuthors();

            if (!String.IsNullOrEmpty(filter))
            {
                Authors = Authors.Where(a => IsInsensitiveString(a.Name, filter) ||
                IsInsensitiveString(a.Surname, filter));
            }

            return View(Authors);
        }

        // GET: Author/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Author/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AuthorId,Name,Surname,Description")] Author author)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _authorRepository.InsertAuthor(author);
                    _authorRepository.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }
            return View();
        }

        // GET: Author/Edit/5
        public ActionResult Edit(int id)
        {
            Author author = _authorRepository.GetAuthorById(id);
            return View(author);
        }

        // POST: Author/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AuthorId,Name,Surname,Description")] Author author)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _authorRepository.UpdateAuthor(author);
                    _authorRepository.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }
            return View();
        }

        // GET: Author/Delete/5
        public ActionResult Delete(int id)
        {
            Author author = _authorRepository.GetAuthorById(id);

            if (author == null)
                return new HttpNotFoundResult();
            
            return View(author);
        }

        // POST: Author/Delete/5
        [HttpPost]
        public ActionResult Delete(Author author)
        {
            try
            {
                _authorRepository.DeleteAuthor(author.AuthorId);
                _authorRepository.Save();
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                //return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        // GET: Author/Details/5
        public ViewResult Details(int id)
        {
            Author author = _authorRepository.GetAuthorById(id);
            return View(author);
        }
    }
}