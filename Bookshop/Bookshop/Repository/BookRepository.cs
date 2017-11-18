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
            Book book = _database.Books.Find(bookId);
            _database.Books.Remove(book);
        }

        public Book GetBookById(int bookId)
        {
            return _database.Books.Include(b => b.Author).FirstOrDefault(b => b.Id == bookId);
        }

        public IEnumerable<Book> GetBooks()
        {
            return _database.Books.Include(b => b.Author).ToList();
        }

        public int InsertBook(Book book)
        {
            _database.Books.Add(book);
            return book.Id;
        }

        public void Save()
        {
            _database.SaveChanges();
        }

        public void UpdateBook(Book book)
        {
            _database.Entry(book).State = EntityState.Modified;
        }
    }
}