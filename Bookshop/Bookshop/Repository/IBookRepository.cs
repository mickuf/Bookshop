﻿using Bookshop.Models;
using System.Collections.Generic;

namespace Bookshop.Repository
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetBooks();
        Book GetBookById(int bookId);
        void CreateBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(int bookId);
    }
}