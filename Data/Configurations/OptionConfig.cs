using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zkiosk.Data.Models;

namespace Zkiosk.Data.Configurations
{
    public class OptionConfig : IEntityTypeConfiguration<Option>
    {
        public void Configure(EntityTypeBuilder<Option> builder)
        {
            // Indecies
            builder.HasIndex(v => new { v.ProductId, v.Name })
                .IsUnique();
            
            // Constraints & Types
            builder.Property(v => v.Name)
                .IsRequired()
                .HasColumnType("nvarchar(150)");
            builder.Property(v => v.ProductId)
                .IsRequired();
            
            // Relations
            builder.HasMany(v => v.Values)
                .WithOne(vv => vv.Option)
                .HasForeignKey(vv => vv.OptionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}