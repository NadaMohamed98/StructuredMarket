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

        }
    }
}
