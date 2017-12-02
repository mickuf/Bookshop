using Bookshop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Bookshop.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BookshopDbContext _bookshopDbContext;
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public AuthorRepository(BookshopDbContext bookshopDbContext)
        {
            _bookshopDbContext = bookshopDbContext;
        }

        public void DeleteAuthor(int authorId)
        {
            try
            {
                Log.DebugFormat("Trying find author in database with id: {0}", authorId);
                Author author = _bookshopDbContext.Authors.Find(authorId);

                if (author == null)
                {
                    Log.Warn("Author object is null!!!");
                    return;
                }

                _bookshopDbContext.Authors.Remove(author);
                _bookshopDbContext.SaveChanges();
                Log.DebugFormat("Author with id: {0} successfully removed from database", authorId);
            }
            catch(Exception ex)
            {
                Log.Warn("Can't delete Author form database!", ex);
            }

        }

        public Author GetAuthorById(int authorId)
        {
            Log.DebugFormat("Finding author in database with id: {0}", authorId);
            return _bookshopDbContext.Authors.Include(a => a.Book).FirstOrDefault(a => a.AuthorId == authorId);
        }

        public IEnumerable<Author> GetAuthors()
        {
            Log.Debug("Finding all authors in database");
            return _bookshopDbContext.Authors.OrderBy(x => x.Name).ToList();
        }

        public IEnumerable<SelectListItem> GetAuthorsSelectList()
        {
            List<SelectListItem> authorsSelectList = new List<SelectListItem>();

            try
            {
                Log.Debug("Trying create AuthorsSelectList from authors in database");
                foreach (Author author in _bookshopDbContext.Authors.ToList())
                {
                    authorsSelectList.Add(new SelectListItem()
                    {
                        Text = author.Name + " " + author.Surname,
                        Value = author.AuthorId.ToString()
                    });
                }
            }
            catch(Exception ex)
            {
                Log.Warn("Can't create AuthorsSelectList!", ex);
            }

            return authorsSelectList;
        }

        public void CreateAuthor(Author author)
        {
            try
            {
                Log.DebugFormat("Trying add author: {0} {1} to database", author.Name, author.Surname);
                _bookshopDbContext.Authors.Add(author);
                _bookshopDbContext.SaveChanges();
                Log.DebugFormat("Author: {0} {1} added to database successfully!", author.Name, author.Surname);
            }
            catch(Exception ex)
            {
                Log.Warn("Can't add author to database!", ex);
            }

        }

        public void UpdateAuthor(Author author)
        {
            try
            {
                Log.DebugFormat("Trying update author {0} {1} with id: {2} in database", author.Name, author.Surname, author.AuthorId);
                _bookshopDbContext.Entry(author).State = EntityState.Modified;
                _bookshopDbContext.SaveChanges();
                Log.DebugFormat("Author {0} {1} with id: {2} updated in database successfully!", author.Name, author.Surname, author.AuthorId);
            }
            catch(Exception ex)
            {
                Log.Warn("Can't update author in database!", ex);
            }
        }
    }
}