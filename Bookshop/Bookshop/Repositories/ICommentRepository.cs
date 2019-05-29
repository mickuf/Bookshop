using Bookshop.Models;

namespace Bookshop.Repositories
{
    public interface ICommentRepository
    {
        void CreateComment(AuthorComment comment);
        void CreateComment(BookComment comment);
    }
}
