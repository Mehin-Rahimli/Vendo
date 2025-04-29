using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendo.Domain.Entities;

namespace Vendo.Persistence.Configurations
{
    internal class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            

                builder.Property(x => x.Name)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                builder
                    .HasIndex(x => x.Name)
                    .IsUnique();
            
        }
    }
}
