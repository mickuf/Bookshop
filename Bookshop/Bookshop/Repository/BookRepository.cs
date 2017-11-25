using Bookshop.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Bookshop.Repository
{
    public class BookRepository : IBookRepository
    {
        private BookshopDbContext _database;

        public BookRepository(BookshopDbContext database)
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