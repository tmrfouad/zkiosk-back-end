using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Zkiosk.Data.Configurations;
using Zkiosk.Data.Models;

namespace Zkiosk.Data
{
    public class ZkioskContext : IdentityDbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<OptionValue> OptionValues { get; set; }
        public DbSet<Variant> Variants { get; set; }
        public DbSet<VariantValue> VariantValues { get; set; }

        public ZkioskContext(DbContextOptions<ZkioskContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProductConfig());
            builder.ApplyConfiguration(new OptionConfig());
            builder.ApplyConfiguration(new OptionValueConfig());
            builder.ApplyConfiguration(new VariantConfig());
            builder.ApplyConfiguration(new VariantValueConfig());

            base.OnModelCreating(builder);
        }
    }
}