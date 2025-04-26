using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vendo.Domain.Entities;

namespace Vendo.Persistence.Configurations
{
    internal class SizeConfiguration
    {
        public void Configure(EntityTypeBuilder<Size> builder)
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
