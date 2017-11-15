using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bookshop.Models;
using System.Data.Entity;

namespace Bookshop.Repository
{
    public class BookRepository : IBookRepository
    {
        private DatabaseBookshop _database;

        public BookRepository(DatabaseBookshop database)
        {
            _database = database;
        }

        public void DeleteBook(int bookId)
        {
            throw new NotImplementedException();
        }

        public Book GetBookById(int bookId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetBooks()
        {
            return _database.Books.Include(b => b.Author).ToList();
        }

        public void InsertBook(Book book)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void UpdateBook(Book book)
        {
            throw new NotImplementedException();
        }
    }
}