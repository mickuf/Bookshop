using Bookshop.Models;
using System;

namespace Bookshop.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly BookshopDbContext _bookshopDbContext;
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public CommentRepository(BookshopDbContext bookshopDbContext)
        {
            _bookshopDbContext = bookshopDbContext;
        }

        public void CreateComment(AuthorComment comment)
        {
            try
            {
                _bookshopDbContext.AuthorComments.Add(comment);
                _bookshopDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Warn("Can't add comment to database!", ex);
            }
        }

        public void CreateComment(BookComment comment)
        {
            try
            {
                _bookshopDbContext.BookComments.Add(comment);
                _bookshopDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Warn("Can't add comment to database!", ex);
            }
        }
    }
}