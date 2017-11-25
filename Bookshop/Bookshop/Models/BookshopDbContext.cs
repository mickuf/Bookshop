using System.Data.Entity;

namespace Bookshop.Models
{
    public class BookshopDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}