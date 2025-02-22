using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace StructuredMarket.Infrastructure.Data
{
    public class StructuredMarketDbContextFactory : IDesignTimeDbContextFactory<StructuredMarketDbContext>
    {
        public StructuredMarketDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Ensure it's pointing to API project
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<StructuredMarketDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);

            return new StructuredMarketDbContext(optionsBuilder.Options);
        }
    }
}
