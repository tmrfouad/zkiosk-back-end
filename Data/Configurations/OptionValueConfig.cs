using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zkiosk.Data.Models;

namespace Zkiosk.Data.Configurations
{
    public class OptionValueConfig : IEntityTypeConfiguration<OptionValue>
    {
        public void Configure(EntityTypeBuilder<OptionValue> builder)
        {
            // Indecies
            builder.HasIndex(v => new { v.OptionId, v.Value })
                .IsUnique();
            
            // Constraints & Types
            builder.Property(v => v.Value)
                .IsRequired()
                .HasColumnType("nvarchar(150)");
            builder.Property(v => v.OptionId)
                .IsRequired();
        }
    }
}