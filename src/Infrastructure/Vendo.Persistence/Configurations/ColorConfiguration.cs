using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vendo.Domain.Entities;


namespace Vendo.Persistence.Configurations
{
    internal class ColorConfiguration : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder
                .Property(p => p.Name)
                .HasColumnType("varchar(100)");

            builder
                .HasIndex(p => p.Name)
                .IsUnique();
                
        }
    }
}
