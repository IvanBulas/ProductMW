using Microsoft.EntityFrameworkCore;

namespace ProductMW
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Product { get; set; }
        public DbSet<Product> Products { get; set; }
       //ublic object Product { get; internal set; }
    }
}
