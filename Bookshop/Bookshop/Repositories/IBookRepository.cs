using Bookshop.Models;
using System.Collections.Generic;

namespace Bookshop.Repositories
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetBooks();
        Book GetBookById(int bookId);
        int CreateBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(int bookId);
    }
}