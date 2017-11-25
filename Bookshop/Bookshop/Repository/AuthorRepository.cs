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

        public AuthorRepository(BookshopDbContext database)
        {
            _bookshopDbContext = database;
        }

        public void DeleteAuthor(int authorId)
        {
            Author author = _bookshopDbContext.Authors.Find(authorId);
            _bookshopDbContext.Authors.Remove(author);
            _bookshopDbContext.SaveChanges();
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
            List<SelectListItem> authors = new List<SelectListItem>();

            foreach (Author author in _bookshopDbContext.Authors.ToList())
            {
                authors.Add(new SelectListItem()
                {
                    Text = author.Name + " " + author.Surname,
                    Value = author.AuthorId.ToString()
                });
            }

            return authors;
        }

        public void InsertAuthor(Author author)
        {
            try
            {
                _bookshopDbContext.Authors.Add(author);
                _bookshopDbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        public void UpdateAuthor(Author author)
        {
            _bookshopDbContext.Entry(author).State = EntityState.Modified;
            _bookshopDbContext.SaveChanges();
        }
    }
}