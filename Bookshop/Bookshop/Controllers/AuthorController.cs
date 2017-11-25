using Bookshop.Models;
using Bookshop.Repository;
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
            IEnumerable<Author> Authors = _authorRepository.GetAuthors();

            if (Authors == null)
            {
                
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author author)
        {   
                try
                {
                    if (ModelState.IsValid)
                    {
                        _authorRepository.CreateAuthor(author);
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

        public ActionResult Edit(int id)
        {
            Author author = _authorRepository.GetAuthorById(id);

            if (author == null)
                return new HttpNotFoundResult();

            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Author author)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _authorRepository.UpdateAuthor(author);
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

        public ActionResult Delete(int id)
        {
            Author author = _authorRepository.GetAuthorById(id);

            if (author == null)
                return new HttpNotFoundResult();

            return View(author);
        }

        [HttpPost]
        public ActionResult Delete(Author author)
        {
            try
            {
                _authorRepository.DeleteAuthor(author.AuthorId);
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
            Author author = _authorRepository.GetAuthorById(id);

            if (author == null)
                return new HttpNotFoundResult();

            return View(author);
        }
    }
}