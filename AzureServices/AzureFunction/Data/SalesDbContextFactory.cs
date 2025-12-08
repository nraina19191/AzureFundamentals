using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunction.Data
{
    public class SalesDbContextFactory : IDesignTimeDbContextFactory<SalesDbContext>
    {
        public SalesDbContext CreateDbContext(string[] args)
        {
            // Build configuration
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("local.settings.json")
                .Build();

            // Get connection string
            var connectionString = configuration.GetSection("Values:DefaultConnection").Value;

            // Configure DbContext options
            var optionsBuilder = new DbContextOptionsBuilder<SalesDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new SalesDbContext(optionsBuilder.Options);
        }
    }
}
