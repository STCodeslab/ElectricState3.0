using ElectricState.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectricState.DataContext
{
    public class ElectricDbContext : DbContext
    {
        public ElectricDbContext(DbContextOptions<ElectricDbContext>options):base (options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }
    }
}
