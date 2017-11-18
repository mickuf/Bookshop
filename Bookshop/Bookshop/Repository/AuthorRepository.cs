using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bookshop.Models;
using System.Data.Entity;

namespace Bookshop.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private DatabaseBookshop _database;

        public AuthorRepository(DatabaseBookshop database)
        {
            _database = database;
        }

        public void DeleteAuthor(int authorId)
        {
            Author author = _database.Authors.Find(authorId);
            _database.Authors.Remove(author);
        }

        public Author GetAuthorById(int authorId)
        {
            return _database.Authors.Find(authorId);
        }

        public IEnumerable<Author> GetAuthors()
        {
            return _database.Authors.ToList();
        }

        public void InsertAuthor(Author author)
        {
            try
            {
                _database.Authors.Add(author);
                _database.SaveChanges();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        public void Save()
        {
            _database.SaveChanges();
        }

        public void UpdateAuthor(Author author)
        {
            _database.Entry(author).State = EntityState.Modified;
        }
    }
}