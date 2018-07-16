using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zkiosk.Data.Models;

namespace Zkiosk.Data.Configurations
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Indecies
            builder.HasIndex(p => p.Title)
                .IsUnique();
            
            // Constraints & Types
            builder.Property(p => p.Title)
                .IsRequired()
                .HasColumnType("nvarchar(150)");
            builder.Property(p => p.Description)
                .IsRequired()
                .HasColumnType("nvarchar(500)");
            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            builder.Property(p => p.SKU)
                .IsRequired()
                .HasColumnType("nvarchar(150)");
            builder.Property(p => p.Barcode)
                .IsRequired()
                .HasColumnType("nvarchar(150)");
            builder.Property(p => p.ImageId)
                .HasColumnType("nvarchar(150)");

            // Relations
            builder.HasMany(p => p.Options)
                .WithOne(v => v.Product)
                .HasForeignKey(v => v.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(p => p.Variants)
                .WithOne(v => v.Product)
                .HasForeignKey(v => v.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}