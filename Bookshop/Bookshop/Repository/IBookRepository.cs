using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bookshop.Models;

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