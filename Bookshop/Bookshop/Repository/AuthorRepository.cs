using Bookshop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Bookshop.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BookshopDbContext _bookshopDbContext;

        public AuthorRepository(BookshopDbContext bookshopDbContext)
        {
            _bookshopDbContext = bookshopDbContext;
        }

        public void DeleteAuthor(int authorId)
        {
            try
            {
                Author author = _bookshopDbContext.Authors.Find(authorId);
                _bookshopDbContext.Authors.Remove(author);
                _bookshopDbContext.SaveChanges();
            }
            catch(Exception ex)
            {

            }

        }

        public Author GetAuthorById(int authorId)
        {
            return _bookshopDbContext.Authors.Find(authorId);
        }

        public IEnumerable<Author> GetAuthors()
        {
            return _bookshopDbContext.Authors.ToList();
        }

        public IEnumerable<SelectListItem> GetAuthorsSelectList()
        {
            List<SelectListItem> AuthorsSelectList = new List<SelectListItem>();

            try
            {
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

            }

            return AuthorsSelectList;
        }

        public void CreateAuthor(Author author)
        {
            try
            {
                _bookshopDbContext.Authors.Add(author);
                _bookshopDbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                
            }

        }

        public void UpdateAuthor(Author author)
        {
            try
            {
                _bookshopDbContext.Entry(author).State = EntityState.Modified;
                _bookshopDbContext.SaveChanges();
            }
            catch(Exception ex)
            {

            }
        }
    }
}