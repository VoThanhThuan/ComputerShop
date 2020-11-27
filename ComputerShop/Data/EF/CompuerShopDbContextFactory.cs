using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Dashboard.Data.EF
{
    public class CompuerShopDbContextFactory : IDesignTimeDbContextFactory<CompuerShopDbContext>
    {
        public CompuerShopDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("ShopManagerDatabase");

            var optionsBuilder = new DbContextOptionsBuilder<CompuerShopDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new CompuerShopDbContext(optionsBuilder.Options);
        }
    }
}
