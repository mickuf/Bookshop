using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Bookshop.Models
{
    public class DatabaseBookshop : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        public void TestDate()
        {
            Authors.Add(new Author() { Name = "George", Surname = "Martin" });

            Books.Add(new Book() { Title = "Gra o Tron", AuthorId = 1, ISBN = "9788365676078", PublicationDate = new DateTime(1996, 08, 1) });
            Books.Add(new Book() { Title = "Starcie Królów", AuthorId = 1, ISBN = "9788365676078", PublicationDate = new DateTime(1998, 11, 16) });
            Books.Add(new Book() { Title = "Nawałnica mieczy. Stal i śnieg", AuthorId = 1, ISBN = "9788365676078", PublicationDate = new DateTime(2000, 08, 8) });
            Books.Add(new Book() { Title = "Nawałnica mieczy. Krew i złoto", AuthorId = 1, ISBN = "9788365676078", PublicationDate = new DateTime(2005, 10, 17) });

            SaveChanges();
        }
    }
}