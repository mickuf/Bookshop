using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bookshop.Repository;
using Bookshop.Models;
using System.Data;

namespace Bookshop.Controllers
{
    public class AuthorController : Controller
    {
        private IAuthorRepository _authorRepository;

        public AuthorController()
        {
            _authorRepository = new AuthorRepository(new DatabaseBookshop());
        }

        public AuthorController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        // GET: Author
        public ActionResult Index()
        {
            IEnumerable<Author> Authors = _authorRepository.GetAuthors();

            return View(Authors);
        }

        // GET: Author/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Author/Create
        [HttpPost]
        public ActionResult Create(Author author)
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
    }
}