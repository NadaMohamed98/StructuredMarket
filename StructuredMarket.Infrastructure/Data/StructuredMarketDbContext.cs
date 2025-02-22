using Microsoft.EntityFrameworkCore;
using StructuredMarket.Domain.Entities;

namespace StructuredMarket.Infrastructure.Data
{
    public class StructuredMarketDbContext : DbContext
    {
        public StructuredMarketDbContext(DbContextOptions<StructuredMarketDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StructuredMarketDbContext).Assembly);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany();

            modelBuilder.Entity<Role>()
                .HasMany(r => r.Permissions)
                .WithMany();

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)  // OrderItem has one Order
                .WithMany(o => o.OrderItems) // Order has many OrderItems
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<Role>().HasData(
            new Role { Id = new Guid("154B4249-0E3D-4D4B-92DC-81C6B2E330CD"), Name = "ADMIN" },
            new Role { Id = new Guid("66266CD5-785D-4CBC-9A87-47E533812885"), Name = "USER" }
        );
        }
    }
}
