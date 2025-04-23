using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendo.Domain.Entities;

namespace Vendo.Persistence.Configurations
{
    internal class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(a => a.Name).IsRequired().HasColumnType("varchar(50)");
            builder.Property(a => a.Surname).IsRequired().HasColumnType("varchar(50)");

        }
    }
}
