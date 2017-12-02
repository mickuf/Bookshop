using Bookshop.Models;
using Bookshop.Repositories;
using Bookshop.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bookshop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AuthorController : Controller
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly ISearchUtility _searchUtility;
        private readonly IImageFileUtility _imageFileUtility;
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private const string DefaultImagePath = "\\Content\\images\\default.png";
        private const string ImageFolderUrl = "~/Content/images/";

        public AuthorController()
        {
            _authorRepository = new AuthorRepository(new BookshopDbContext());
            _searchUtility = new SearchUtilty();
            _imageFileUtility = new ImageFileUtility();
        }

        public AuthorController(IAuthorRepository authorRepository, ISearchUtility searchUtility, IImageFileUtility imageFileUtility)
        {
            _authorRepository = authorRepository;
            _searchUtility = searchUtility;
            _imageFileUtility = imageFileUtility;
        }

        [AllowAnonymous]
        public ActionResult Index(string filter)
        {
            Log.DebugFormat("GET Index with filter: {0}", filter);
            IEnumerable<Author> authors = _authorRepository.GetAuthors();

            if (authors == null)
            {
                Log.Warn("Authors list is null!");
                return new HttpNotFoundResult();
            }

            if (!String.IsNullOrEmpty(filter))
            {
                authors = authors.Where(a => _searchUtility.IsInsensitiveString(a.Name, filter) ||
                _searchUtility.IsInsensitiveString(a.Surname, filter));
            }

            return View(authors);
        }

        public ActionResult Create()
        {
            Log.Debug("GET Create");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author author)
        {   
            try
            {
                Log.DebugFormat("POST Create with author: {0} {1}", author.Name, author.Surname);

                if (ModelState.IsValid)
                {
                    Log.DebugFormat("Is model valid: {0}", ModelState.IsValid);

                    HttpPostedFileBase file = Request.Files[0];

                    author.ImagePath = _imageFileUtility.SaveImageFileInPath(file, ImageFolderUrl);

                    _authorRepository.CreateAuthor(author);

                    return RedirectToAction("Details", new { id = author.AuthorId });
                }
            }
            catch (Exception ex)
            {
                Log.Warn("Create Author failed!", ex);
                ModelState.AddModelError(string.Empty, "Nie można zapisać zmian, spróbuj ponowanie");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            Log.DebugFormat("GET Edit with id: {0}", id);
            Author author = _authorRepository.GetAuthorById(id);

            if (author == null)
            {
                Log.Warn("Author object is null!");
                return new HttpNotFoundResult();
            }

            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Author author)
        {
            try
            {
                Log.DebugFormat("POST Edit with author: {0} {1} with id: {2}", author.Name, author.Surname, author.AuthorId);

                if (ModelState.IsValid)
                {
                    Log.DebugFormat("Is model valid: {0}", ModelState.IsValid);

                    HttpPostedFileBase file = Request.Files[0];

                    if (file == null || file.ContentLength == 0)
                    {
                        Log.Debug("User do not want change picture");
                    }
                    else
                    {
                        _imageFileUtility.DeleteImageFromPath(author.ImagePath, DefaultImagePath);

                        author.ImagePath = _imageFileUtility.SaveImageFileInPath(file, ImageFolderUrl);
                    }

                    _authorRepository.UpdateAuthor(author);
                    return RedirectToAction("Details", new { id = author.AuthorId});
                }
            }
            catch (Exception ex)
            {
                Log.Warn("Update Author failed!", ex);
                ModelState.AddModelError(string.Empty, "Nie można zaktualizować autora, spróbuj ponownie");
            }
            return View(author);
        }

        public ActionResult Delete(int id)
        {
            Log.DebugFormat("GET Delete with id: {0}", id);
            Author author = _authorRepository.GetAuthorById(id);

            if (author == null)
            {
                Log.Warn("Author object is null!");
                return new HttpNotFoundResult();
            }

            return View(author);
        }

        [HttpPost]
        public ActionResult Delete(Author author)
        {
            try
            {
                Log.DebugFormat("POST Delete with author: {0} {1} with id: {2}", author.Name, author.Surname, author.AuthorId);

                _imageFileUtility.DeleteImageFromPath(author.ImagePath, DefaultImagePath);

                _authorRepository.DeleteAuthor(author.AuthorId);
            }
            catch (Exception ex)
            {
                Log.Warn("Delete Author failed!", ex);
                ModelState.AddModelError(string.Empty, "Nie można usunąć autora, spróbuj ponownie");
            }
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            Log.DebugFormat("GET Details with id: {0}", id);
            Author author = _authorRepository.GetAuthorById(id);

            if (author == null)
            {
                Log.Warn("Author object is null!");
                return new HttpNotFoundResult();
            }          
            return View(author);
        }
    }
}