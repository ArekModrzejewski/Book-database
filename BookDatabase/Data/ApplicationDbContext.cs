using BookDatabase.Models;
using Microsoft.EntityFrameworkCore;

namespace BookDatabase.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<BookModel> Book { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
    }
}
