using Microsoft.EntityFrameworkCore;
using StackBuildApi.Model.Entities;

namespace StackBuildApi.Data.Database
{
    public class StackBuildDB : DbContext 
    {

            public StackBuildDB(DbContextOptions<StackBuildDB> options)
                : base(options) { }

            public DbSet<Product> Products { get; set; }
            public DbSet<Order> Orders { get; set; }
            public DbSet<OrderItem> OrderItems { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
               

            base.OnModelCreating(modelBuilder);

            // set some constraints & indexes for production-readiness
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Name);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.Items)
                .WithOne(i => i.Order)
                .HasForeignKey(i => i.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
        }
}
