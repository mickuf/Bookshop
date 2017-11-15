using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bookshop.Models;

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
            throw new NotImplementedException();
        }

        public Author GetAuthorById(int authorId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Author> GetAuthors()
        {
            return _database.Authors.ToList();
        }

        public void InsertAuthor(Author author)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void UpdateAuthor(Author author)
        {
            throw new NotImplementedException();
        }
    }
}