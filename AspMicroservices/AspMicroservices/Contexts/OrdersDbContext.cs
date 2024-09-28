using Microsoft.EntityFrameworkCore;
using SharedModels;

namespace OrderService.Contexts
{
    public class OrdersDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public OrdersDbContext(DbContextOptions<OrdersDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().ToTable("Orders");
        }
    }

}