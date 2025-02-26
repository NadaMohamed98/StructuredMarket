using Microsoft.EntityFrameworkCore;
using StructuredMarket.Domain.Entities;

namespace StructuredMarket.Infrastructure.Data
{
    public class StructuredMarketDbContext : DbContext
    {
        public StructuredMarketDbContext(DbContextOptions<StructuredMarketDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StructuredMarketDbContext).Assembly);
            base.OnModelCreating(modelBuilder);

            // Many-to-Many: User <-> Role
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            // Many-to-Many: Role <-> Permission
            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId);

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)  // OrderItem has one Order
                .WithMany(o => o.OrderItems) // Order has many OrderItems
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = new Guid("11111111-1111-1111-1111-111111111111"), Name = "ADMIN" },
                new Role { Id = new Guid("22222222-2222-2222-2222-222222222222"), Name = "USER" }
            );

            // Define Product IDs
            var laptopId = Guid.NewGuid();
            var smartphoneId = Guid.NewGuid();
            var headphonesId = Guid.NewGuid();
            var smartwatchId = Guid.NewGuid();

            // Define Order IDs
            var order1Id = Guid.NewGuid();
            var order2Id = Guid.NewGuid();

            var userId = Guid.NewGuid(); // Example User ID

            // Seed User
            modelBuilder.Entity<User>().HasData(
                new User
                (
                    userId,
                     "nada",
                    "mohamed",
                    "nadaMohamed",
                    "nada@gmail.com",
                    "123456789",
                    "lK93K78vTMYYLwk0C/ccytNjZ8GXIXpuda/rE98OCe5wV2F6uI5HX3cqUHZtZZFi" // nada123
                )
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = laptopId, Name = "Laptop", Price = 1500.00m },
                new Product { Id = smartphoneId, Name = "Smartphone", Price = 800.00m },
                new Product { Id = headphonesId, Name = "Headphones", Price = 200.00m },
                new Product { Id = smartwatchId, Name = "Smartwatch", Price = 300.00m }
            );

            // Seed Orders
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = order1Id,
                    UserId = userId,
                    DeliveryAddress = "123 Main St, City",
                    TotalAmount = 1700.00m,
                    DeliveryTime = DateTime.UtcNow.AddDays(3)
                },
                new Order
                {
                    Id = order2Id,
                    UserId = userId,
                    DeliveryAddress = "456 Elm St, Town",
                    TotalAmount = 1100.00m,
                    DeliveryTime = DateTime.UtcNow.AddDays(5)
                }
            );

            // Seed Order Items (Referencing Product IDs)
            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem { Id = Guid.NewGuid(), OrderId = order1Id, ProductId = laptopId, Quantity = 1 },
                new OrderItem { Id = Guid.NewGuid(), OrderId = order1Id, ProductId = headphonesId, Quantity = 1 },
                new OrderItem { Id = Guid.NewGuid(), OrderId = order2Id, ProductId = smartphoneId, Quantity = 1 }
            );

        }
    }
}
