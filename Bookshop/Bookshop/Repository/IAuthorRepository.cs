using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bookshop.Models;

namespace Bookshop.Repository
{
    public interface IAuthorRepository
    {
        IEnumerable<Author> GetAuthors();
        Author GetAuthorById(int authorId);
        void InsertAuthor(Author author);
        void UpdateAuthor(Author author);
        void DeleteAuthor(int authorId);
        void Save();
    }
}