using Bookshop.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Bookshop.Repository
{
    public interface IAuthorRepository
    {
        IEnumerable<Author> GetAuthors();
        Author GetAuthorById(int authorId);
        IEnumerable<SelectListItem> GetAuthorsSelectList();
        void InsertAuthor(Author author);
        void UpdateAuthor(Author author);
        void DeleteAuthor(int authorId);
    }
}