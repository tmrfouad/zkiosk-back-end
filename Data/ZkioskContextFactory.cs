using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace Zkiosk.Data
{
    public class ZkioskContextFactory : IDesignTimeDbContextFactory<ZkioskContext>
    {
        public static IConfiguration Configuration { get; set; }
        public ZkioskContextFactory()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

        public ZkioskContext CreateDbContext(string[] args)
        {
            var connection = Configuration.GetConnectionString("ZkioskDatabase");
            var optionsBuilder = new DbContextOptionsBuilder<ZkioskContext>();
            optionsBuilder.UseSqlServer(connection);

            return new ZkioskContext(optionsBuilder.Options);
        }
    }
}