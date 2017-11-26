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
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public AuthorRepository(BookshopDbContext bookshopDbContext)
        {
            _bookshopDbContext = bookshopDbContext;
        }

        public void DeleteAuthor(int authorId)
        {
            try
            {
                _log.DebugFormat("Trying find author in database with id: {0}", authorId);
                Author author = _bookshopDbContext.Authors.Find(authorId);
                _bookshopDbContext.Authors.Remove(author);
                _bookshopDbContext.SaveChanges();
                _log.DebugFormat("Author with id: {0} successfully removed from database", authorId);
            }
            catch(Exception ex)
            {
                _log.Warn("Can't delete Author form database!", ex);
            }

        }

        public Author GetAuthorById(int authorId)
        {
            _log.DebugFormat("Finding author in database with id: {0}", authorId);
            return _bookshopDbContext.Authors.Find(authorId);
        }

        public IEnumerable<Author> GetAuthors()
        {
            _log.Debug("Finding all authors in database");
            return _bookshopDbContext.Authors.ToList();
        }

        public IEnumerable<SelectListItem> GetAuthorsSelectList()
        {
            List<SelectListItem> AuthorsSelectList = new List<SelectListItem>();

            try
            {
                _log.Debug("Trying create AuthorsSelectList from authors in database");
                foreach (Author author in _bookshopDbContext.Authors.ToList())
                {
                    AuthorsSelectList.Add(new SelectListItem()
                    {
                        Text = author.Name + " " + author.Surname,
                        Value = author.AuthorId.ToString()
                    });
                }
            }
            catch(Exception ex)
            {
                _log.Warn("Can't create AuthorsSelectList!", ex);
            }

            return AuthorsSelectList;
        }

        public void CreateAuthor(Author author)
        {
            try
            {
                _log.DebugFormat("Trying add author: {0} {1} to database", author.Name, author.Surname);
                _bookshopDbContext.Authors.Add(author);
                _bookshopDbContext.SaveChanges();
                _log.DebugFormat("Author: {0} {1} added to database successfully!", author.Name, author.Surname);
            }
            catch(Exception ex)
            {
                _log.Warn("Can't add author to database!", ex);
            }

        }

        public void UpdateAuthor(Author author)
        {
            try
            {
                _log.DebugFormat("Trying update author {0} {1} with id: {2} in database", author.Name, author.Surname, author.AuthorId);
                _bookshopDbContext.Entry(author).State = EntityState.Modified;
                _bookshopDbContext.SaveChanges();
                _log.DebugFormat("Author {0} {1} with id: {2} updated in database successfully!", author.Name, author.Surname, author.AuthorId);
            }
            catch(Exception ex)
            {
                _log.Warn("Can't update author in database!", ex);
            }
        }
    }
}