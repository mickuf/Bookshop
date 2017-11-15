using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bookshop.Repository;
using Bookshop.Models;

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
    }
}