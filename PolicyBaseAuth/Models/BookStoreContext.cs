using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolicyBaseAuth.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summery { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
    public class BookStoreContext:IdentityDbContext
    {
        public DbSet<Book> Books { get; set; }

        public BookStoreContext(DbContextOptions<BookStoreContext> options):base(options)
        {

        }
    }
}
