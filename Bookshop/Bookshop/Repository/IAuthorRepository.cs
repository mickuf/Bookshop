using Bookshop.Models;
using System.Collections.Generic;

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