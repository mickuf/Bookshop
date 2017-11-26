using Bookshop.Models;
using Bookshop.Repositories;
using Bookshop.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Bookshop.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly ISearchUtility _searchUtility;
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public AuthorController()
        {
            _authorRepository = new AuthorRepository(new BookshopDbContext());
            _searchUtility = new SearchUtilty();
        }

        public AuthorController(IAuthorRepository authorRepository, ISearchUtility searchUtility)
        {
            _authorRepository = authorRepository;
            _searchUtility = searchUtility;
        }


        public ActionResult Index(string filter)
        {
            _log.DebugFormat("GET Index with filter: {0}", filter);
            IEnumerable<Author> Authors = _authorRepository.GetAuthors();

            if (Authors == null)
            {
                _log.Warn("Authors list is null!");
                return new HttpNotFoundResult();
            }

            if (!String.IsNullOrEmpty(filter))
            {
                Authors = Authors.Where(a => _searchUtility.IsInsensitiveString(a.Name, filter) ||
                _searchUtility.IsInsensitiveString(a.Surname, filter));
            }

            return View(Authors);
        }

        public ActionResult Create()
        {
            _log.Debug("GET Create");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author author)
        {
            _log.DebugFormat("POST Create with author: {0} {1}", author.Name, author.Surname);
            try
            {
                if (ModelState.IsValid)
                {
                    _log.DebugFormat("Is model valid: {0}", ModelState.IsValid);
                    _authorRepository.CreateAuthor(author);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                _log.Warn("Create Author failed!", ex);
                ModelState.AddModelError(string.Empty, "Nie można zapisać zmian, spróbuj ponowanie");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            _log.DebugFormat("GET Edit with id: {0}", id);
            Author author = _authorRepository.GetAuthorById(id);

            if (author == null)
            {
                _log.Warn("Author object is null!");
                return new HttpNotFoundResult();
            }

            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Author author)
        {
            _log.DebugFormat("POST Edit with author: {0} {1} with id: {0}", author.Name, author.Surname, author.AuthorId);
            try
            {
                if (ModelState.IsValid)
                {
                    _log.DebugFormat("Is model valid: {0}", ModelState.IsValid);
                    _authorRepository.UpdateAuthor(author);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                _log.Warn("Update Author failed!", ex);
                ModelState.AddModelError(string.Empty, "Nie można zaktualizować autora, spróbuj ponownie");
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            _log.DebugFormat("GET Delete with id: {0}", id);
            Author author = _authorRepository.GetAuthorById(id);

            if (author == null)
            {
                _log.Warn("Author object is null!");
                return new HttpNotFoundResult();
            }

            return View(author);
        }

        [HttpPost]
        public ActionResult Delete(Author author)
        {
            _log.DebugFormat("POST Delete with author: {0} {1} with id: {2}", author.Name, author.Surname, author.AuthorId);
            try
            {
                _authorRepository.DeleteAuthor(author.AuthorId);
            }
            catch (Exception ex)
            {
                _log.Warn("Delete Author failed!", ex);
                ModelState.AddModelError(string.Empty, "Nie można usunąć autora, spróbuj ponownie");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            _log.DebugFormat("GET Details with id: {0}", id);
            Author author = _authorRepository.GetAuthorById(id);

            if (author == null)
            {
                _log.Warn("Author object is null!");
                return new HttpNotFoundResult();
            }          
            return View(author);
        }
    }
}