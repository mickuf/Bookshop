using Bookshop.Models;
using System.Collections.Generic;

namespace Bookshop.Repository
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetBooks();
        Book GetBookById(int bookId);
        int InsertBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(int bookId);
        void Save();
    }
}