using Bookshop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Bookshop.Repository
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
                Book book = _bookshopDbContext.Books.Find(bookId);
                _bookshopDbContext.Books.Remove(book);
                _bookshopDbContext.SaveChanges();
            }
            catch(Exception ex)
            {

            }
        }

        public Book GetBookById(int bookId)
        {
            return _bookshopDbContext.Books.Include(b => b.Author).FirstOrDefault(b => b.Id == bookId);
        }

        public IEnumerable<Book> GetBooks()
        {
            return _bookshopDbContext.Books.Include(b => b.Author).ToList();
        }

        public void CreateBook(Book book)
        {
            try
            {
                _bookshopDbContext.Books.Add(book);
                _bookshopDbContext.SaveChanges();
            }
            catch(Exception ex)
            {

            }
        }

        public void UpdateBook(Book book)
        {
            try
            {
                _bookshopDbContext.Entry(book).State = EntityState.Modified;
                _bookshopDbContext.SaveChanges();
            }
            catch(Exception ex)
            {

            }
        }
    }
}