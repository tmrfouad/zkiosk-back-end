using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zkiosk.Data.Models;

namespace Zkiosk.Data.Configurations
{
    public class VariantValueConfig : IEntityTypeConfiguration<VariantValue>
    {
        public void Configure(EntityTypeBuilder<VariantValue> builder)
        {
            // Indecies
            builder.HasIndex(p => new { p.VariantId, p.ValueId })
                .IsUnique();
            
            // Constraints & Types
            builder.Property(p => p.OptionId)
                .IsRequired();
            builder.Property(p => p.ValueId)
                .IsRequired();
            builder.Property(p => p.VariantId)
                .IsRequired();
        }
    }
}