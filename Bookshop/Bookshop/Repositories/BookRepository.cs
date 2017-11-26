using Bookshop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Bookshop.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookshopDbContext _bookshopDbContext;
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public BookRepository(BookshopDbContext bookshopDbContext)
        {
            _bookshopDbContext = bookshopDbContext;
        }

        public void DeleteBook(int bookId)
        {
            try
            {
                _log.DebugFormat("Trying find book in database with id: {0}", bookId);
                Book book = _bookshopDbContext.Books.Find(bookId);
                _bookshopDbContext.Books.Remove(book);
                _bookshopDbContext.SaveChanges();
                _log.DebugFormat("Book with id: {0} successfully removed from database", bookId);
            }
            catch(Exception ex)
            {
                _log.Warn("Can't delete book form database!", ex);
            }
        }

        public Book GetBookById(int bookId)
        {
            _log.DebugFormat("Finding book in database with id: {0}", bookId);
            return _bookshopDbContext.Books.Include(b => b.Author).FirstOrDefault(b => b.Id == bookId);
        }

        public IEnumerable<Book> GetBooks()
        {
            _log.Debug("Finding all books in database");
            return _bookshopDbContext.Books.Include(b => b.Author).ToList();
        }

        public void CreateBook(Book book)
        {
            try
            {
                _log.DebugFormat("Trying add book: {0} to database", book.Title);
                _bookshopDbContext.Books.Add(book);
                _bookshopDbContext.SaveChanges();
                _log.DebugFormat("Book: {0} added to database successfully!", book.Title);
            }
            catch(Exception ex)
            {
                _log.Warn("Can't add book to database!", ex);
            }
        }

        public void UpdateBook(Book book)
        {
            try
            {
                _log.DebugFormat("Trying update book {0} with id: {1} in database", book.Title, book.Id);
                _bookshopDbContext.Entry(book).State = EntityState.Modified;
                _bookshopDbContext.SaveChanges();
                _log.DebugFormat("Book {0} with id: {1} updated in database successfully!", book.Title, book.Id);
            }
            catch(Exception ex)
            {
                _log.Warn("Can't update book in database!", ex);
            }
        }
    }
}