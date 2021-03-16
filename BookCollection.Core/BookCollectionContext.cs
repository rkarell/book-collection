using Microsoft.EntityFrameworkCore;

namespace BookCollection.Core
{
    public class BookCollectionContext : DbContext
    {
        public BookCollectionContext(DbContextOptions<BookCollectionContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
    }
}