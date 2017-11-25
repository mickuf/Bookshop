using Bookshop.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Bookshop.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookshopDbContext _bookshopDbContext;

        public BookRepository(BookshopDbContext database)
        {
            _bookshopDbContext = database;
        }

        public void DeleteBook(int bookId)
        {
            Book book = _bookshopDbContext.Books.Find(bookId);
            _bookshopDbContext.Books.Remove(book);
            _bookshopDbContext.SaveChanges();
        }

        public Book GetBookById(int bookId)
        {
            return _bookshopDbContext.Books.Include(b => b.Author).FirstOrDefault(b => b.Id == bookId);
        }

        public IEnumerable<Book> GetBooks()
        {
            return _bookshopDbContext.Books.Include(b => b.Author).ToList();
        }

        public void InsertBook(Book book)
        {
            _bookshopDbContext.Books.Add(book);
            _bookshopDbContext.SaveChanges();
        }

        public void UpdateBook(Book book)
        {
            _bookshopDbContext.Entry(book).State = EntityState.Modified;
            _bookshopDbContext.SaveChanges();
        }
    }
}