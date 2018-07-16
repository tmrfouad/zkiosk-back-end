using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zkiosk.Data.Models;

namespace Zkiosk.Data.Configurations
{
    public class VariantConfig : IEntityTypeConfiguration<Variant>
    {
        public void Configure(EntityTypeBuilder<Variant> builder)
        {
            // Constraints & Types
            builder.Property(p => p.ProductId)
                .IsRequired();
            builder.Property(p => p.ValuesId)
                .IsRequired();
            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            builder.Property(p => p.SKU)
                .IsRequired()
                .HasColumnType("nvarchar(150)");
            builder.Property(p => p.Barcode)
                .IsRequired()
                .HasColumnType("nvarchar(150)");

            // Relations
            builder.HasMany(v => v.Values)
                .WithOne(v => v.Variant)
                .HasForeignKey(v => v.VariantId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}