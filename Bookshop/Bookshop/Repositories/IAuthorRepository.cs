using Bookshop.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Bookshop.Repositories
{
    public interface IAuthorRepository
    {
        IEnumerable<Author> GetAuthors();
        Author GetAuthorById(int authorId);
        IEnumerable<SelectListItem> GetAuthorsSelectList();
        void CreateAuthor(Author author);
        void UpdateAuthor(Author author);
        void DeleteAuthor(int authorId);
    }
}